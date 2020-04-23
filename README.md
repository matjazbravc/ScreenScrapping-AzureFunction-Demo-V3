# Screen Scrapping Azure Function Demo V3

Azure functions gain its popularity especially with **V3** which brings new capabilities including the ability to target **.NET Core 3.1** and **Node 12**.

This sample is updated version of [project which was made with Azure functions V2](https://github.com/matjazbravc/ScreenScrapping-AzureFunction-Demo). 
For DI I've used [Autofac](https://autofac.org/) extended with excellent [**Autofac.Extensions.DependencyInjection.AzureFunctions Nuget**](https://github.com/junalmeida/autofac-azurefunctions) library made by *Marcos Junior*.
It supports DI as we are used from Asp.Net Core. That means you will be able to create classes with interfaces as parameters of a constructor and passed to the Azure function's constructor just like in a regular Asp.Net Core project. And not just that. Functions are not declared as static anymore. Just perfect!

## Prerequisites
- [Visual Studio](https://www.visualstudio.com/vs/community) 2019 16.5.4 or greater

To create and deploy functions, you also need:
- An active Azure subscription. If you don't have an Azure subscription, [free accounts](https://azure.microsoft.com/en-us/free) are available.
- An Azure Storage account. To create a storage account, see [Create a storage account](https://docs.microsoft.com/en-us/azure/storage/common/storage-create-storage-account#create-a-storage-account).

To get started with Azure Functions, you can visit [Microsoft Azure Functions portal](https://azure.microsoft.com/en-us/services/functions/).

Enjoy!

## Licence

Licenced under [MIT](http://opensource.org/licenses/mit-license.php).
Contact me on [LinkedIn](https://si.linkedin.com/in/matjazbravc).