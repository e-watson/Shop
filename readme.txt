1. Download and unpack project into directory <Project> (directory on your HDD, not root)
2. In all web.config / app.config find the key:

ShopDbContext

and change attachdbfilename=D:\Projects\Shop\Shop.Web\App_Data\Shop.mdf
to attachdbfilename=<Project>\Shop\Shop.Web\App_Data\Shop.mdf

3. Open Shop.sln inside your instance of VS2013 (Pro, as preferable) and set project Shop.Web as startup
4. Select browser for browsing (Chrome, as preferable ;-))
5. Run / Start Debug  - it should start under developer IIS instance