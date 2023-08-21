# HenBot

Add your token to env variables with name "HenBotToken" on [Windows](https://docs.oracle.com/en/database/oracle/machine-learning/oml4r/1.5.1/oread/creating-and-modifying-environment-variables-on-windows.html) on linux edit the /etc/environment file and relogin/reboot if u use mac kys.

Apply migration:

1. Installing the tools:
    
        dotnet tool install --global dotnet-ef
2. Update database:

        dotnet ef database update

Availble commands:

/start - sends start info,

/settings - configure post details. You can configure limit - number of pics per message (10 max) and tags queries (supports almost all of [gelbooru search options](https://gelbooru.com/index.php?page=wiki&s=&s=view&id=26263)).

/getAyaya - sends you InlineKeyboard with your saved tags(if this is configured in /settings) or a 10 latest posts from gelbooru with "rating:general" tag. After choosing an option, you recieve posts according to chosen tag and your settings.

/next - Only available after /getAyaya command. Posts next pics according to the previous /getAyaya settings.
