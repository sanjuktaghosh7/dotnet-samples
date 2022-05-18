﻿// Copyright 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START vault_quickstart]
using Google.Apis.Auth.OAuth2;
using Google.Apis.Vault.v1;
using Google.Apis.Vault.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

namespace VaultQuickstart
{
    // Class to demonstrate the use of Vault list matters API
    class Program
    {
        /* Global instance of the scopes required by this quickstart.
         If modifying these scopes, delete your previously saved token.json/ folder. */
        static string[] Scopes = { VaultService.Scope.EdiscoveryReadonly };
        static string ApplicationName = "Google Vault API .NET Quickstart";

        static void Main(string[] args)
        {
            try
            {
                UserCredential credential;
                // Load client secrets.
                using (var stream =
                       new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    /* The file token.json stores the user's access and refresh tokens, and is created
                     automatically when the authorization flow completes for the first time. */
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Vault API service.
                var service = new VaultService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                // Define request parameters.
                MattersResource.ListRequest request = service.Matters.List();
                request.PageSize = 10;

                // List matters.
                ListMattersResponse response = request.Execute();
                Console.WriteLine("Matters:");
                if (response.Matters != null && response.Matters.Count > 0)
                {
                    foreach (var matter in response.Matters)
                    {
                        Console.WriteLine("{0} ({1})", matter.Name, matter.MatterId);
                    }
                }
                else
                {
                    Console.WriteLine("No matters found.");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
// [END vault_quickstart]
