# HenBot

Paste your bot token from @BotFather into the token field in the Program.cs file

Availble commands:

/start - sends start info,

/settings - configure post details. You can configure limit - number of pics per message (10 max), [rating](https://gelbooru.com/index.php?page=wiki&s=view&id=2535) and tags.

/getAyaya - sends you InlineKeyboard with your saved tags(if this is configured in /settings) or an empty button. After choosing an option, you recieve posts according to chosen tag and your settings, if you haven't completed the configuration, it will send you 10 pics with a General rating with tag `all`.

/next - Only available after /getAyaya command. Posts next pics according to the previous /getAyaya settings.
