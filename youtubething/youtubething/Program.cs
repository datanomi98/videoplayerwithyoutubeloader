using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


namespace youtubething
{

    class Program
    {

        internal class Search
        {
            


            [STAThread]
            static void Main(string[] args)
            {
                //Console.WriteLine("google api testi hässäkkä");

                try
                {
                  
                    new Search().Run().Wait();
                }
                catch (AggregateException ex)
                {
                    foreach (var e in ex.InnerExceptions)
                    {
                        Console.WriteLine("error: " + e.Message);
                    }
                }



            }
            private async Task Run()
            {
               
                var tubeserv = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyDYXG0fLnPQmO1Thxyqe1He7j7tZ2JIpyU",
                    ApplicationName = this.GetType().ToString()
                });

                string path = System.IO.File.ReadAllText(@"D:\c#\filez\search.txt");
                // Console.WriteLine(path);

                var searchListRequest = tubeserv.Search.List("snippet");

                searchListRequest.Q = path; // Replace with your search term.
                searchListRequest.MaxResults = 3;

                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = await searchListRequest.ExecuteAsync();

                List<string> videos = new List<string>();
                List<string> title = new List<string>();
                 

                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.
                foreach (var searchResult in searchListResponse.Items)
                {
                    switch (searchResult.Id.Kind)
                    {
                        
                        case "youtube#video":
                            videos.Add(String.Format("{0}", searchResult.Id.VideoId));
                            title.Add(String.Format("{0}", searchResult.Snippet.Title));
                            break;
                           


                    }
                }
              
                   if(videos.Count == 1)
                {
                    Console.WriteLine(String.Format("Video1:\n{0}\n", string.Join("\n", videos[0])));
                    Console.WriteLine(String.Format("Title1:\n{0}\n", string.Join("\n", title[0])));
                }
                else if (videos.Count == 2)
                {
                    Console.WriteLine(String.Format("Video1:\n{0}\n", string.Join("\n", videos[0])));
                    Console.WriteLine(String.Format("Title1:\n{0}\n", string.Join("\n", title[0])));
                    Console.WriteLine(String.Format("Video2:\n{0}\n", string.Join("\n", videos[1])));
                    Console.WriteLine(String.Format("Title2:\n{0}\n", string.Join("\n", title[1])));
                }
                else if(videos.Count == 3)
                {
                    Console.WriteLine(String.Format("Video1:\n{0}\n", string.Join("\n", videos[0])));
                    Console.WriteLine(String.Format("Title1:\n{0}\n", string.Join("\n", title[0])));
                    Console.WriteLine(String.Format("Video2:\n{0}\n", string.Join("\n", videos[1])));
                    Console.WriteLine(String.Format("Title2:\n{0}\n", string.Join("\n", title[1])));
                    Console.WriteLine(String.Format("Video3:\n{0}\n", string.Join("\n", videos[2])));
                    Console.WriteLine(String.Format("Title3:\n{0}\n", string.Join("\n", title[2])));
                }
                
                Console.Write("\n");
                Console.Write("which one? ");
                string numero = Console.ReadLine();

                if(numero == "1")
                {
                    string firstv = videos[0];
                    
                   System.IO.File.WriteAllText(@"D:\c#\filez\result.txt", firstv);
                }else if(numero == "2")
                {
                    string secondV = videos[1];

                    System.IO.File.WriteAllText(@"D:\c#\filez\result.txt", secondV);
                }
                else
                {
                    string thirdv = videos[2];

                    System.IO.File.WriteAllText(@"D:\c#\filez\result.txt", thirdv);
                }
   
                    
                
                
                    
                   
                   




                }
            }
        }
    }

