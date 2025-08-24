using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirebaseController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public FirebaseController()
        {
            string projectId = "vns-firebase-storage";
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        [HttpPost("add-file-info")]
        public async Task<IActionResult> AddFileInfo([FromBody] FileInfoModel model)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection("files").Document();
                Dictionary<string, object> data = new()
                {
                    { "FileName", model.FileName },
                    { "Url", model.Url },
                    { "UploadedAt", Timestamp.GetCurrentTimestamp() }
                };

                await docRef.SetAsync(data);

                return Ok(new { message = "File info added successfully", id = docRef.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    public class FileInfoModel
    {
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}
