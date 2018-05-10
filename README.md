# 百度AI开放平台 .Net SDK

**支持平台：.Net Framework 3.5/4.0/4.5 , Core 2.0**

**目录结构**

        AipSdk.Baidu.Aip
        ├── Ocr                              //文字识别
        ├── Face                             //人脸识别
        ├── Nlp                              //语言处理基础技术
        ├── ContentCensor                    //内容审核
        ├── ImageClassify                    //图像识别
        ├── ImageSearch                      //图像搜索
        ├── Kg                               //知识图谱
        └── Speech                           //语音合成&语音识别

工程请使用vs2017打开。

## 使用方法

### 使用NuGet管理（推荐）

在NuGet中搜索 `Baidu.AI`，安装最新版即可。

地址 https://www.nuget.org/packages/Baidu.AI/


### 下载dll导入

1.在[官方网站](http://ai.baidu.com/sdk)下载C# SDK压缩工具包。

2.解压后，将对应平台的 `AipSdk.dll` 添加为引用, 并添加[Json.net](https://www.newtonsoft.com/json)引用。




## 接口文档



接口文档参考[百度AI开放平台官方文档](http://ai.baidu.com/docs)