using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GeoAnalyst;

public class testshp : MonoBehaviour
{
    public string filePath;
    public ISurfaceOp2 surfaceOp2;       //表面分析操作对象
    public IGeoDataset inputDataset;   //输入数据集
    public IGeoDataset outputDataset;   //输出数据集

    void Start()
    {
        //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
       
    }
    void UpDate()
    {
       
    }
    public void EventSystem()
    {
        GetRasterByName();
        RasterSurfaceAnalysis();
    }
    void GetRasterByName()
    {
        string filePath = "D:/test";
        string Name = "shanghai.tif";
        IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactory();
        IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(filePath, 0);
        IRasterWorkspace pRasterWorkspace = pWorkspace as IRasterWorkspace;
        IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(Name);
        IRaster pRaster = pRasterDataset.CreateDefaultRaster();
        inputDataset = pRaster as IGeoDataset;
    }
    void RasterSurfaceAnalysis()
    {
        surfaceOp2 = new RasterSurfaceOpClass();
        outputDataset = surfaceOp2.Aspect(inputDataset);
        string directoryPath = "D:/test";
        string fileName = "test";
        IWorkspaceFactory wf = new RasterWorkspaceFactoryClass();
        IWorkspace ws = wf.OpenFromFile(directoryPath, 0) as IWorkspace;
        IConversionOp converop = new RasterConversionOpClass();
        converop.ToRasterDataset(outputDataset, "TIFF", ws, fileName);

        IRasterLayer rlayer = new RasterLayerClass();
        IRaster raster = new Raster();
        raster = outputDataset as IRaster;
        rlayer.CreateFromRaster(raster);       //使用raster对象创建一个rasterLayer对象
        rlayer.Name = fileName;    //设置图层名字
    }
}
