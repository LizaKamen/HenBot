# HenBot

```bash
cd HenBot
```

Create `appsettings.json` based on example and fill all properties. Get `botToken` from `@BotFather` in Telegram. `api_key` and `user_id` from [options](https://gelbooru.com/index.php?page=account&s=options) after you login in(yeah you need an account).

## Local

Run

```bash
dotnet run
```

## Docker

```bash
docker-compose up --build
```

Availble commands:

`/start` - sends start info.

`/settings` - configure post details. You can configure limit - number of pics per message (10 max) and tags queries (supports almost all of [gelbooru search options](https://gelbooru.com/index.php?page=wiki&s=&s=view&id=26263)).

`/getAyaya` - sends you InlineKeyboard with your saved tags(if this is configured in `/settings`) or a 10 latest posts from gelbooru with "rating:general" tag. After choosing an option, you receive posts according to chosen tag and your settings.

`/next` - Only available after `/getAyaya` command. Posts next pics according to the previous `/getAyaya` settings.

`/getRandomTag` - get pics by random tag.
