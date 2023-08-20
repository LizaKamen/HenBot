# HenBot

Add your token to env variables with name "HenBotToken" or just paste it to token var in Program.cs (linux moment).

Availble commands:

/start - sends start info,

/settings - configure post details. You can configure limit - number of pics per message (10 max) and tags queries (supports almost all of [gelbooru search options](https://gelbooru.com/index.php?page=wiki&s=&s=view&id=26263)).

/getAyaya - sends you InlineKeyboard with your saved tags(if this is configured in /settings) or a 10 latest posts from gelbooru. After choosing an option, you recieve posts according to chosen tag and your settings.

/next - Only available after /getAyaya command. Posts next pics according to the previous /getAyaya settings.
