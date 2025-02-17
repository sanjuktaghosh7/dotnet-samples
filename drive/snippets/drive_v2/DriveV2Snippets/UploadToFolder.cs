// Copyright 2022 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START drive_upload_to_folder]

using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using File = Google.Apis.Drive.v2.Data.File;

namespace DriveV2Snippets
{
    // Class to demonstrate use of Drive upload to folder.
    public class UploadToFolder
    {
        /// <summary>
        /// Upload a file to the specified folder.
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns>Inserted file metadata if successful, null otherwise</returns>

        public File DriveUploadToFolder(string folderId)
        {
            try
            {
                /* Load pre-authorized user credentials from the environment.
                 TODO(developer) - See https://developers.google.com/identity for
                 guides on implementing OAuth2 for your application. */
                GoogleCredential credential = GoogleCredential.GetApplicationDefault()
                    .CreateScoped(DriveService.Scope.Drive);

                // Create Drive API service.
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive API Snippets"
                });
                var fileMetadata = new File()
                {
                    Title = "photo.jpg",
                    Parents = new List<ParentReference>
                    {
                        new ParentReference()
                        {
                            Id = folderId
                        }
                    }
                };
                FilesResource.InsertMediaUpload request;
                // Create a new file on drive.
                using (var stream = new FileStream("files/photo.jpg", FileMode.Open))
                {
                    // Create a new file, with metadata and stream
                    request = service.Files.Insert(
                        fileMetadata, stream, "image/jpeg");
                    request.Fields = "id";
                    request.Upload();
                }
                var file = request.ResponseBody;
                // Prints the uploaded file id.
                Console.WriteLine("File ID: " + file.Id);
                return file;
            }
            catch (Exception e)
            {
                // TODO(developer) - handle error appropriately
                if (e is AggregateException)
                {
                    Console.WriteLine("Credential Not found");
                }
                else if (e is GoogleApiException)
                {
                    Console.WriteLine(" Failed With an Error {0}",e.Message);
                }
                
                else
                {
                    throw;
                }
            }
            return null;
        }
    }
}
// [END drive_upload_to_folder]