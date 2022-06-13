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

using DriveV3Snippets;
using NUnit.Framework;

namespace DriveV3SnippetsTest
{
    // Unit testcase for drive v3 fetch changes snippet
    [TestFixture]
    public class FetchChangesTest : BaseTest
    {
        //TODO(developer) - Provide absolute path of the file
        private string filePath = "files/photo.jpg";
        
        [Test]
        public void TestFetchChanges()
        {
            string startPageToken = FetchStartPageToken.DriveFetchStartPageToken();
            var id = CreateTestBlob(filePath);
            string newStartPageToken = FetchChanges.DriveFetchChanges(startPageToken);
            Assert.IsNotNull(newStartPageToken);
            Assert.AreNotEqual(startPageToken, newStartPageToken);
            service.Files.Delete(id);
        }
    }
}