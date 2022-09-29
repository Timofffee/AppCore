# General Information
Vendor: Krem
<br>
Package Name: AzureAnalytics
<br>
Author: Sergey Liuminarskiy <theprideme@gmail.com>
<br>
Use package with Unity 2020.3.25 or Later
<br>
Use Core Version: 0.11.2
<br><br>
Package create for fast and simple integration in game Azure Game SDK stack:<br>
- Facebook SDK
- AppsFlyer SDK
- AppMetrica SDK
- AppLovin Max SDK

Package contains event actions for support Azure Guidelines
<br>
https://docs.google.com/spreadsheets/d/1eeU46jdKNCtpG7DHX3XqxndS9ZLtGneeM5unIONBffQ/edit#gid=0
<br>
And actions for Ads placement


# Prepare for Use
Import external-dependency-manager-1.2.158
and add selected registries
<br>
![Add Google to Unity package manager](./PrepareForUse_1.png)

Update External Dependency Manager to Latest version (Currently worked on 1.2.163)
<br>
![Update External Dependency Manager](./PrepareForUse_2.png)
<br>
<br>
<br>

#### IMPORTANT!
#### AFTER IMPORT External Dependency Manager FROM GOOGLE REGISTRY
#### DO NOT IMPORT IT FROM OTHER SDK AND PACKAGES
<br>
<br>
<br>

## Import and setup Facebook

Download latest facebook sdk version (currently worked on 9.0.0)
<br>
https://developers.facebook.com/docs/unity/

Import Facebook sdk
<br>
**BE SURE TO DISCARD FOR IMPORT PlayServiceResolver AND External Dependency Manager
from facebook sdk. because it already installed from google official repository**
<br>
![Do not import](./ImportFacebook_1.png)

Disable Android Auto Resolution if that window has appear after import facebook sdk

After that setup all facebook settings:<br>
Facebook > Edit Settings

## Import and setup AppsFlyer

Download latest version of AppsFlyer SDK (currently worked on 6.1.4)<br>
https://github.com/AppsFlyerSDK/appsflyer-unity-plugin
<br>
**BE SURE TO DISCARD FOR IMPORT PlayServiceResolver AND External Dependency Manager**
<br>
![Do not import](./ImportAppsFlyer_1.png)

## Import and setup AppMetrica
Download latest version of AppMetrica SDK (currently worked on 3.7.0)<br>
https://appmetrica.yandex.ru/docs/mobile-sdk-dg/concepts/unity-plugin.html

## Import and setup AppLovin MaxSDK
Download latest version of AppLovin Max SDK (Currently worked on 3.2.3)
<br>
**BE SURE TO DISCARD FOR IMPORT PlayServiceResolver AND External Dependency Manager**
<br>
![Do not import](./ImportMaxSDK_1.png)

# Cleanup

## Android resolver
Delete all files in folder Plugins/Android
<br>
Assets > External Dependency Manager > Android Resolver > Delete Resolved Libraries
<br>
Assets > External Dependency Manager > Android Resolver > Force Resolve