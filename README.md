# Zyronator

Demonstration of .Net C# API hosted at Azure.  It is based around the [Discogs lists of DJ Zyron](https://www.discogs.com/user/Zyron/lists?page=1&limit=100&header=1).

Current commands:

http://zyronator.azurewebsites.net/api/UserLists/Zyron

Fetches Zyron's lists directly via Discogs API.

http://zyronator.azurewebsites.net/api/UserLists/Zyron/database

Fetches Zyron's lists from SQL Server database at Azure (may be different to API list).

http://zyronator.azurewebsites.net/api/UserLists/Synchronize

Synchronizes SQL Server database lists with Discogs API lists.  Will add new lists to the database.


