using EDSU_SYSTEM.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EDSU_SYSTEM.Controllers
{
    [Authorize(Roles = "superAdmin")]
    public class Canvas : Controller
    {
        private readonly ApplicationDbContext _context;

        const int perPage = 100; // Number of courses per page
        const int UserperPage = 100; // Number of users per page
        int currentPage = 1; // Start with the first page
        int UsercurrentPage = 1; // Start with the first page
        bool hasMore = true;
        bool hasMoreUser = true;
        private readonly HttpClient client;
        public Canvas(ApplicationDbContext context)
        {
            _context = context;
            client = new HttpClient
            {
                BaseAddress = new Uri(Environment.GetEnvironmentVariable("CANVAS_BASE_URL"))
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("CANVAS_TOKEN"));
        }
        public async Task<IActionResult> GetCanvasUsers(string id)
        {
            string sisCourseId = id;
            Console.WriteLine(sisCourseId);
            string url = $"https://{Environment.GetEnvironmentVariable("CANVAS_DOMAIN")}/api/v1/courses/sis_course_id:{sisCourseId}/users?enrollment_type[]=student&include[]=enrollments&per_page=1000&access_token={Environment.GetEnvironmentVariable("CANVAS_TOKEN")}";
         
            HttpResponseMessage response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();

            // Parse the response into a JSON object
            string jsonString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);
            Console.WriteLine(jsonObject);
            
            return View();
        }
        public async Task<List<User>> GetAllUsers()
        {
            var allUsers = new List<User>(); // Store all users here

            while (hasMoreUser)
            {
                var url = $"{Environment.GetEnvironmentVariable("CANVAS_API_URL")}/accounts/{Environment.GetEnvironmentVariable("CANVAS_ACCOUNT_ID")}/users?per_page={UserperPage}&page={UsercurrentPage}&include[]=email";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(responseBody);

                    allUsers.AddRange(users);

                    // Check if there are more users to retrieve
                    var linkHeader = response.Headers.GetValues("Link").FirstOrDefault();
                    if (!string.IsNullOrEmpty(linkHeader))
                    {
                        var hasNextPage = linkHeader.Contains("rel=\"next\"");
                        hasMoreUser = hasNextPage;
                        UsercurrentPage++;
                    }
                    else
                    {
                        hasMoreUser = false;
                    }
                }
                else
                {
                    Console.WriteLine("Error retrieving users: " + response.StatusCode);
                    break;
                }
            }
            return allUsers;
        }
        public async Task<List<CanvasCourse>> GetAllCourses()
        {
            var allCourses = new List<CanvasCourse>(); // Store all courses here

            while (hasMore)
            {
                var url = $"{Environment.GetEnvironmentVariable("CANVAS_API_URL")}/accounts/{Environment.GetEnvironmentVariable("CANVAS_ACCOUNT_ID")}/courses?per_page={perPage}&page={currentPage}&include[]=sis_course_id";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var courses = JsonConvert.DeserializeObject<List<CanvasCourse>>(responseBody);

                    allCourses.AddRange(courses);

                    // Check if there are more courses to retrieve
                    var linkHeader = response.Headers.GetValues("Link").FirstOrDefault();
                    if (!string.IsNullOrEmpty(linkHeader))
                    {
                        var hasNextPage = linkHeader.Contains("rel=\"next\"");
                        hasMore = hasNextPage;
                        currentPage++;
                    }
                    else
                    {
                        hasMore = false;
                    }
                }
                else
                {
                    Console.WriteLine("Error retrieving courses: " + response.StatusCode);
                    break;
                }
            }

            Console.WriteLine($"Retrieved {allCourses.Count} courses:");

            foreach (var course in allCourses)
            {
                
                Console.WriteLine($"{course.Id} {course.Sis_course_id} ({course.Name})");
            }
            return allCourses;
        }
        public async Task EnrollStudent(string id)
        {
            var users = await GetAllUsers();
            var canvasCourses = await GetAllCourses();
            
            var currentSession = _context.Sessions.FirstOrDefault(x => x.IsActive == true);
            var courses = (from s in _context.CourseRegistrations where s.Students.SchoolEmailAddress == id && s.SessionId == currentSession.Id && s.Status == Models.Enum.MainStatus.Approved select s.Courses.Code).ToList();
            var student = (from x in users where x.Email == id select x.Email).FirstOrDefault();
            Console.WriteLine("This is the student ID: " + student);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("CANVAS_BASE_URL"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("CANVAS_TOKEN"));

                foreach (var item in courses)
                {
                   
                    var course_id = (from s in canvasCourses where s.Sis_course_id == item select s.Id).FirstOrDefault();
                    Console.WriteLine("This is the course ID: " + course_id);

                    var enrollment = new
                    {
                        enrollment = new
                        {
                            user_id = student,
                            type = "StudentEnrollment",
                            enrollment_state = "active"
                        }
                    };

                    var json = JsonConvert.SerializeObject(enrollment);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"/api/v1/courses/{course_id}/enrollments", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to enroll user in course: {response.StatusCode}");
                    }
                }
            }
        }public async Task EnrollStaff(string id)
        {
            var users = await GetAllUsers();
            var canvasCourses = await GetAllCourses();
            
            var currentSession = _context.Sessions.FirstOrDefault(x => x.IsActive == true);
            var courses = (from s in _context.CourseAllocations where s.Staff.SchoolEmail == id && s.Courses.SessionId == currentSession.Id  select s.Courses.Code).ToList();
            var staff = (from x in users where x.Email == id select x.Id).FirstOrDefault();
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("CANVAS_BASE_URL"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("CANVAS_TOKEN"));

                foreach (var item in courses)
                {
                   
                    var course_id = (from s in canvasCourses where s.Sis_course_id == item select s.Id).FirstOrDefault();
                    
                    var enrollment = new
                    {
                        enrollment = new
                        {
                            user_id = staff,
                            type = "StudentEnrollment",
                            enrollment_state = "active"
                        }
                    };

                    var json = JsonConvert.SerializeObject(enrollment);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"/api/v1/courses/{course_id}/enrollments", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to enroll user in course: {response.StatusCode}");
                    }
                }
            }
        }

    }
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string User_id { get; set; }
        public string LoginId { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
    }
    public class CanvasCourse
    {
        public string Id { get; set; }
        public string Sis_course_id { get; set; }
        public string Name { get; set; }
        public string Course_id { get; set; }
        public bool IsActive { get; set; }
    }
}
