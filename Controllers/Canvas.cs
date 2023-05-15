using CanvasApi.Client.Users.Models;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace EDSU_SYSTEM.Controllers
{
    
    public class Canvas : Controller
    {
        private readonly ApplicationDbContext _context;

        const string token = "13819~l0NzZI7bdk8V5p1uNgHgAQHuXFCTWsKYopkeLcDqAtKm56szgfBRWuxhuxyhQOv0";
        const string testKey = "3m9sr9wH1ShA0CptyyjjSiRaJRWevULibq5EhgRLxManWaVeubtUlEWGeHPtluOE";
        const string domain = "edouniversity.instructure.com"; 
        const string baseUrl = "https://edouniversity.instructure.com";
        //HttpClient client = new HttpClient();
        private readonly HttpClient client;
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        public Canvas(ApplicationDbContext context)
        {
            _context = context; 
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<IActionResult> GetCanvasUsers(string id)
        {
            string sisCourseId = id;
            Console.WriteLine(sisCourseId);
            string url = $"https://{domain}/api/v1/courses/sis_course_id:{sisCourseId}/users?enrollment_type[]=student&include[]=enrollments&per_page=1000&access_token={token}";
         
            HttpResponseMessage response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();

            // Parse the response into a JSON object
            string jsonString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);
            Console.WriteLine(jsonObject);
            
            return View();
        }
        
        public async Task EnrollStudent(string id)
        {
            var currentSession = _context.Sessions.FirstOrDefault(x => x.IsActive == true);
            var courses = (from s in _context.CourseRegistrations where s.Students.SchoolEmailAddress == id && s.SessionId == currentSession.Id && s.Status == Models.Enum.MainStatus.Approved select s.Courses.Code).ToList();
            var student = _context.Students.FirstOrDefault(x => x.SchoolEmailAddress == id);
            //var res_courses = await client.GetAsync($"https://edouniversity.instructure.com/api/v1/courses");

            //if (res_courses.IsSuccessStatusCode)
            //{
            //    var responseBody = await res_courses.Content.ReadAsStringAsync();
            //    Console.WriteLine(responseBody);
            //}
            //else
            //{
            //    Console.WriteLine("Error: " + res_courses.StatusCode);
            //}
            foreach (var item in courses)
            {
                var enrollment = new
                {
                    enrollment = new
                    {
                        user_id = "6860",
                        type = "StudentEnrollment",
                        enrollment_state = "active"
                    }
                };
                
                var json = JsonConvert.SerializeObject(enrollment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await client.PostAsync($"/api/v1/courses/3436/enrollments", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to enroll user in course: {response.StatusCode}");
                    }
                }
            }
        }

    }
}
