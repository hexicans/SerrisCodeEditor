﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SerrisTabsServer.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace SerrisTabsServer.Manager
{
    public static class TabsAccessManager
    {
        static StorageFile file = AsyncHelpers.RunSync(() => ApplicationData.Current.LocalFolder.CreateFileAsync("tabs_list.json", CreationCollisionOption.OpenIfExists).AsTask());

        public static async Task<List<InfosTab>> GetTabsAsync(int id)
        {
            using (var reader = new StreamReader(await file.OpenStreamForReadAsync()))
            using (JsonReader JsonReader = new JsonTextReader(reader))
            {
                try
                {
                    List<TabsList> list = new JsonSerializer().Deserialize<List<TabsList>>(JsonReader);
                    if (list != null)
                    {
                        return list.Where(m => m.ID == id).FirstOrDefault().tabs.Where(n => n.TabInvisibleByDefault == false).ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }

        }

        public static async Task<List<int>> GetTabsListIDAsync()
        {

            using (var reader = new StreamReader(await file.OpenStreamForReadAsync()))
            using (JsonReader JsonReader = new JsonTextReader(reader))
            {
                try
                {
                    List<TabsList> list = new JsonSerializer().Deserialize<List<TabsList>>(JsonReader);
                    var list_ids = new List<int>();

                    if (list != null)
                    {
                        foreach (TabsList list_tabs in list)
                        {
                            list_ids.Add(list_tabs.ID);
                        }
                        return list_ids;
                    }
                    else
                    {
                        return list_ids;
                    }
                }
                catch
                {
                    return null;
                }
            }

        }

        public static async Task<List<int>> GetTabsIDAsync(int id_list)
        {

            using (var reader = new StreamReader(await file.OpenStreamForReadAsync()))
            using (JsonReader JsonReader = new JsonTextReader(reader))
            {
                try
                {
                    List<TabsList> list = new JsonSerializer().Deserialize<List<TabsList>>(JsonReader);
                    var list_ids = new List<int>();

                    if (list != null)
                    {
                        if (list.Where(m => m.ID == id_list).FirstOrDefault().tabs != null)
                        {
                            foreach (InfosTab tab in list.Where(m => m.ID == id_list).FirstOrDefault().tabs)
                            {
                                if (!tab.TabInvisibleByDefault)
                                {
                                    list_ids.Add(tab.ID);
                                }
                            }
                        }

                        return list_ids;
                    }
                    else
                    {
                        return list_ids;
                    }
                }
                catch
                {
                    return null;
                }
            }

        }

        public static List<int> GetTabsID(int id_list)
        {

            using (var reader = new StreamReader(AsyncHelpers.RunSync(() => file.OpenStreamForReadAsync())))
            using (JsonReader JsonReader = new JsonTextReader(reader))
            {
                try
                {
                    List<TabsList> list = new JsonSerializer().Deserialize<List<TabsList>>(JsonReader);
                    var list_ids = new List<int>();

                    if (list != null)
                    {
                        if (list.Where(m => m.ID == id_list).FirstOrDefault().tabs != null)
                        {
                            foreach (InfosTab tab in list.Where(m => m.ID == id_list).FirstOrDefault().tabs)
                            {
                                if (!tab.TabInvisibleByDefault)
                                {
                                    list_ids.Add(tab.ID);
                                }
                            }
                        }

                        return list_ids;
                    }
                    else
                    {
                        return list_ids;
                    }
                }
                catch
                {
                    return null;
                }
            }

        }

        public static async Task<InfosTab> GetTabViaIDAsync(TabID id)
        {

            using (var reader = new StreamReader(await file.OpenStreamForReadAsync()))
            using (JsonReader JsonReader = new JsonTextReader(reader))
            {
                try
                {
                    List<TabsList> list = new JsonSerializer().Deserialize<List<TabsList>>(JsonReader);

                    if (list != null)
                    {
                        if (list.Where(m => m.ID == id.ID_TabsList).FirstOrDefault().tabs != null)
                        {
                            return list.Where(m => m.ID == id.ID_TabsList).FirstOrDefault().tabs.Where(m => m.ID == id.ID_Tab).FirstOrDefault();
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public static InfosTab GetTabViaID(TabID id)
        {

            using (var reader = new StreamReader(AsyncHelpers.RunSync(() => file.OpenStreamForReadAsync())))
            using (JsonReader JsonReader = new JsonTextReader(reader))
            {
                try
                {
                    List<TabsList> list = new JsonSerializer().Deserialize<List<TabsList>>(JsonReader);

                    if (list != null)
                    {
                        if (list.Where(m => m.ID == id.ID_TabsList).FirstOrDefault().tabs != null)
                        {
                            return list.Where(m => m.ID == id.ID_TabsList).FirstOrDefault().tabs.Where(m => m.ID == id.ID_Tab).FirstOrDefault();
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public static async Task<TabsList> GetTabsListViaIDAsync(int id)
        {

            using (var reader = new StreamReader(await file.OpenStreamForReadAsync()))
            using (JsonReader JsonReader = new JsonTextReader(reader))
            {
                try
                {
                    List<TabsList> list = new JsonSerializer().Deserialize<List<TabsList>>(JsonReader);

                    if (list != null)
                    {
                        if (list.Where(m => m.ID == id).FirstOrDefault().tabs != null)
                        {
                            return list.Where(m => m.ID == id).FirstOrDefault();
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public static async Task<string> GetTabContentViaIDAsync(TabID id)
        {
            try
            {
                StorageFolder folder_content = await ApplicationData.Current.LocalFolder.CreateFolderAsync("tabs", CreationCollisionOption.OpenIfExists);
                StorageFile file_content = await folder_content.GetFileAsync(id.ID_TabsList + "_" + id.ID_Tab + ".json");

                using (var reader = new StreamReader(await file_content.OpenStreamForReadAsync()))
                using (JsonReader JsonReader = new JsonTextReader(reader))
                {
                    try
                    {
                        ContentTab content = new JsonSerializer().Deserialize<ContentTab>(JsonReader);

                        if (content != null)
                        {
                            return content.Content;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static void UpdateTab(ref InfosTab tab, int id_list)
        {

            using (var reader = new StreamReader(file.OpenStreamForReadAsync().Result))
            {
                try
                {
                    int id = tab.ID;
                    JObject tabs = JObject.Parse(reader.ReadToEnd()).Values<JObject>().Where(m => m["ID"].Value<int>() == id_list).FirstOrDefault(),
                        tab_search = tabs.Values<JObject>().Where(m => m["ID"].Value<int>() == id).FirstOrDefault();

                    if (tab != null)
                    {
                        tab = tab_search.Value<InfosTab>();
                    }
                }
                catch { }
            }

        }

    }
}
