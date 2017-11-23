﻿using log4net;
using Nini.Config;
using OpenMetaverse;
using OpenSim.Framework;
using OpenSim.Framework.Servers.HttpServer;
using OpenSim.Region.Framework.Scenes;
using OpenSim.Region.OptionalModules.RegionsDataPublisher.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenSim.Region.OptionalModules.RegionsDataPublisher
{
    class RegionsDataHandler : BaseStreamHandler
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IConfigSource m_config;
        private List<Scene> m_scenes = new List<Scene>();

        public RegionsDataHandler(IConfigSource config, ref List<Scene> scenes) : base("GET", "/RegionData/")
        {
            if (config != null)
                m_config = config;

            m_scenes = scenes;
        }

        protected override byte[] ProcessRequest(string path, Stream requestData, IOSHttpRequest httpRequest, IOSHttpResponse httpResponse)
        {
            Dictionary<string, object> request = new Dictionary<string, object>();
            foreach (string name in httpRequest.QueryString)
                request[name] = httpRequest.QueryString[name];

            httpResponse.ContentType = "application/json";

            mainDataSet _dataSet = new mainDataSet();

            foreach(Scene _scene in m_scenes)
            {
                regionDataSet _regionData = new regionDataSet();

                _regionData.RegionName = _scene.Name;
                _regionData.RegionEstate = _scene.RegionInfo.EstateSettings.EstateName;
                _regionData.RegionImageUUID = _scene.RegionInfo.lastMapUUID.ToString();
                _regionData.RegionPosition = _scene.RegionInfo.WorldLocX + "/" + _scene.RegionInfo.WorldLocY;
                _regionData.RegionPublicAccess = _scene.RegionInfo.EstateSettings.PublicAccess;
                _regionData.RegionSize = _scene.RegionInfo.RegionSizeX.ToString();
                _regionData.RegionUUID = _scene.RegionInfo.RegionID.ToString();
                _regionData.RegionIsVisibleInSearch = true;

                _dataSet.RegionData.Add(_regionData);

                List<ILandObject> _landData = _scene.LandChannel.AllParcels();
                foreach(ILandObject _parcel in _landData)
                {
                    parcelDataSet _parcelSet = new parcelDataSet();

                    _parcelSet.ParcelName = _parcel.LandData.Name;
                    _parcelSet.ParcelDescription = _parcel.LandData.Description;
                    _parcelSet.ImageUUID = _parcel.LandData.SnapshotID.ToString();
                    _parcelSet.ParcelDwell = (int)_parcel.LandData.Dwell;
                    _parcelSet.ParcelOwnerUUID = _parcel.LandData.OwnerID.ToString();
                    _parcelSet.ParcelGroup = _parcel.LandData.GroupID.ToString();
                    _parcelSet.ParcelBitmap = Convert.ToBase64String(_parcel.LandData.Bitmap);
                    _parcelSet.ParcelPrice = _parcel.LandData.SalePrice;
                    _parcelSet.ParcelIsVisibleInSearch = getStatusForSearch(_parcel);
                    _parcelSet.ParcelIsForSale = getStatusForSale(_parcel);
                    _parcelSet.ParentUUID = _scene.RegionInfo.RegionID.ToString();
                    _parcelSet.ParcelUUID = _parcel.LandData.GlobalID.ToString();

                    _dataSet.ParcelData.Add(_parcelSet);
                }

                foreach(SceneObjectGroup _cog in _scene.GetSceneObjectGroups())
                {
                    objectDataSet _objectData = new objectDataSet();

                    _objectData.ObjectName = _cog.Name;
                    _objectData.ObjectDescription = _cog.Description;
                    _objectData.ObjectUUID = _cog.RootPart.UUID.ToString();
                    _objectData.ParentUUID = _scene.LandChannel.GetLandObject(_cog.RootPart.AbsolutePosition.X, _cog.RootPart.AbsolutePosition.Y).LandData.GlobalID.ToString();
                    _objectData.ObjectIsForSale = false;

                    if(_cog.RootPart.ObjectSaleType == 1)
                        _objectData.ObjectIsForSale = true;

                    _objectData.ObjectIsForCopy = getStatusForCopy(_cog);
                    _objectData.ObjectGroupUUID = _cog.GroupID.ToString();
                    _objectData.ObjectItemUUID = _cog.FromItemID.ToString();
                    _objectData.ObjectOwnerUUID = _cog.OwnerID.ToString();
                    _objectData.ObjectPosition = _cog.RootPart.AbsolutePosition.X + "/" + _cog.RootPart.AbsolutePosition.Y + "/" + _cog.RootPart.AbsolutePosition.Z;
                    _objectData.ObjectImageUUID = GuessImage(_cog);

                    _dataSet.ObjectData.Add(_objectData);
                }
            }



            return null;
        }

        private bool getStatusForCopy(SceneObjectGroup prim)
        {
            if (((uint)prim.RootPart.Flags & (uint)PrimFlags.ObjectCopy) != 0)
                return true;

            return false;
        }

        private bool getStatusForSale(ILandObject parcel)
        {
            if ((parcel.LandData.Flags & (uint)ParcelFlags.ForSale) == (uint)ParcelFlags.ForSale)
                return true;

            return false;
        }

        private bool getStatusForSearch(ILandObject parcel)
        {
            if ((parcel.LandData.Flags & (uint)ParcelFlags.ShowDirectory) == (uint)ParcelFlags.ShowDirectory)
                return true;

            return false;
        }

        private string GuessImage(SceneObjectGroup sog)
        {
            string bestguess = string.Empty;
            Dictionary<UUID, int> counts = new Dictionary<UUID, int>();

            PrimitiveBaseShape shape = sog.RootPart.Shape;
            if (shape != null && shape.ProfileShape == ProfileShape.Square)
            {
                Primitive.TextureEntry textures = shape.Textures;
                if (textures != null)
                {
                    if (textures.DefaultTexture != null &&
                        textures.DefaultTexture.TextureID != UUID.Zero &&
                        textures.DefaultTexture.RGBA.A < 50f)
                    {
                        counts[textures.DefaultTexture.TextureID] = 8;
                    }

                    if (textures.FaceTextures != null)
                    {
                        foreach (Primitive.TextureEntryFace tentry in textures.FaceTextures)
                        {
                            if (tentry != null)
                            {
                                if (tentry.TextureID != UUID.Zero && tentry.TextureID != UUID.Zero && tentry.TextureID != UUID.Zero && tentry.RGBA.A < 50)
                                {
                                    int c = 0;
                                    counts.TryGetValue(tentry.TextureID, out c);
                                    counts[tentry.TextureID] = c + 1;
                                    // decrease the default texture count
                                    if (counts.ContainsKey(textures.DefaultTexture.TextureID))
                                        counts[textures.DefaultTexture.TextureID] = counts[textures.DefaultTexture.TextureID] - 1;
                                }
                            }
                        }
                    }

                    // Let's pick the most unique texture
                    int min = 9999;
                    foreach (KeyValuePair<UUID, int> kv in counts)
                    {
                        if (kv.Value < min && kv.Value >= 1)
                        {
                            bestguess = kv.Key.ToString();
                            min = kv.Value;
                        }
                    }
                }
            }

            return bestguess;
        }
    }
}