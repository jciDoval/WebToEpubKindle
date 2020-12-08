![CodeQL](https://github.com/jciDoval/WebToEpubKindle/workflows/CodeQL/badge.svg?branch=master)
![.NET Core](https://github.com/jciDoval/WebToEpubKindle/workflows/.NET%20Core/badge.svg?branch=master)

# WebToEpubKindle

This project borns with the idea to provide a simple way to create epub files through a .NET 5 console app. 

This app will have inside differents plugins which will obtain data across internet and with this data the epub will be created.


## Plugins under development
---

### 1. InManga Plugin
---

With this plugin the app will connet to the https://inmanga.com/ website to obtain all the chapters from a manga, specifying by parameters the chapter and manga info.

Once the capture of data finish the console will create the epub file with all of these content.

Later through console parameters the app will permit convert the epub file to a mobi file using Calibre software.

### 2. Rss Plugin
---

This plugin will connect to a rss site to obtain the last articles published to create an epub file with these data.
