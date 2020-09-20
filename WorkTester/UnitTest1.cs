using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace WorkTester
{
    public class UnitTest1
    {

        //Method to check Authentication method in Intimidators Management API
        [Fact]
        public async void Test1()
        {

            using (var Checker = new HttpClient())
            {
                //You have to Sign in or Register to get a user token. Here I'm using a perviously generated one.
                //1# scenario - Correct JWT
                Checker.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWlyVGFoYSBJZDoxMiIsIm5iZiI6MTYwMDYwOTEzNSwiZXhwIjoxNjAwNjk1NTM1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjQ1MDkyLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDUwOTIvIn0.9L5zS7-XdCHiHcN4lhsGg2edEVZbEGF8LoFSgmAWCfGZduYOaOVbQetrlyoierlHJUHxqRXd2Aa5H3_tRtHPWg");



                /*#2 scenario - Wrong JWT
                                 Checker.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "SomeWrongJWT");

                 */


                /*#3 scenario - Null JWT
                                 Checker.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", null);

                 */

                var result =await Checker.GetAsync("https://localhost:44337/api/Home/Authenticate");
                Assert.True(result.IsSuccessStatusCode);

            }


        }
    }
}
