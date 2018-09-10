namespace Endjin.SpecFlow.Selenium.Framework.Reporting
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Endjin.SpecFlow.Selenium.Framework.Environment;

    #endregion

    public class TestStatusReporter
    {
        public Task ReportAsync(string sessionId, string jobName, bool success, Exception error)
        {
            if (TestEnvironment.Current.IsRemote)
            {
                // TeamCity Sauce Labs plugin will expect this line to be in the build log 
                // (which uses the console output) as it parses it as a post build activity 
                // to get results back.
                Console.WriteLine("SauceOnDemandSessionID={0} job-name={1}", sessionId, jobName);
            }
            else
            {
                Trace.TraceInformation("SessionID={0} job-name={1} result={2}", sessionId, jobName, success);
            }

            return UpdateJobStatusAsync(sessionId, jobName, success, error);
        }

        private static async Task UpdateJobStatusAsync(string sessionId, string jobName, bool success, Exception error)
        {
            if (!TestEnvironment.Current.IsRemote)
            {
                return;
            }

            // If we're using the remote driver, we need to update the Sauce Labs job status.
            var username = TestEnvironment.Current.SauceLabsRemoteUsername;
            var key = TestEnvironment.Current.SauceLabsRemoteKey;

            var update = new JobStatusUpdate { Name = jobName, Passed = success };
            if (error != null)
            {
                update.Data = new Dictionary<string, string> { { "error", error.Message } };
            }

            /* From SauceLabs API Reference, example request:
                curl -X PUT https://YOUR_USERNAME:YOUR_ACCESS_KEY@saucelabs.com/rest/v1/YOUR_USERNAME/jobs/YOUR_JOB_ID \
                     -H "Content-Type: application/json" \
                     -d '{"tags": ["test", "example", "taggable"],
                          "public": true,
                          "name": "changed-job-name",
                          "passed": false, 
                          "custom-data": {"error": "step 17 failed"}}'
             */
            var url = string.Format("https://{0}:{1}@saucelabs.com/rest/v1/{0}/jobs/{2}", username, key, sessionId);

            var parameter = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + key));

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", parameter);

                try
                {
                    var result = await client.PutAsJsonAsync(url, update);
                    result.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ERROR] TestResultReporter.UpdateJobStatus{0}{1}", Environment.NewLine, ex);
                }
            }
        }
    }
}