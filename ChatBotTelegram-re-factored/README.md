﻿# ChatBotTelegram-re-factored

---

- Simple Version - https://github.com/russianowner/ChatBotTelegram

---
- Телеграм бот, который ведет диалог с пользователем, в заданном нами настроении. 
- Together Token брать тут - https://api.together.ai/
---
- A telegram bot that conducts a dialogue with the user, in the mood we set. 
- Get Together Tokens here - https://api.together.ai/

---

# О боте:

-  Отвечает пользователю после команды `/start`
-  Сохраняет историю сообщений для контекста диалога
-  Использует **Together AI** (можно использовать много разных моделей)
-  Работает на официальной библиотеке [`Telegram.Bot`]
-  Легкий, асинхронный и готов к деплою
-  Архитектура кода максимально упрощена и разделена на методы для удобства понимания и изменения

---

# About the bot:

- Responds to the user after the command `/start`
- Saves the message history for the context of the dialog
- Uses **Together AI** (many different models can be used)
- Works on the official library ['Telegram.Bot']
- Lightweight, asynchronous and ready for deployment
- The architecture of the code is simplified as much as possible and divided into methods for ease of understanding and modification
	
---

# Что используется / What is used

- .NET 8
- dotnet add package Telegram.Bot
- Together AI API
- dotnet add package System.Text.Json
- dotnet add package System.Net.Http.Json

---

# Как пользоваться ботом?
- Копируем репозиторий
- Заходим в appsettings.json
- Меняем токен бота и апи кей
- Открой файл Services/TogetherService.cs
- Найди var payload, внутри него model, message и т.д - это параметры модели, которые можно менять
- model — выбрать другую модель
- temperature — креативность ответов (0.2 — строгие ответы, 1.0 — креативные)
- max_tokens — максимальная длина ответа
---
- Открой файл Services/HistoryService.cs и найди метод:
- sb.AppendLine("Введи промт настроения"); - это промт настроения, его можно менять на любой другой
- Можно также поменять подписи в истории сообщений:
- sb.AppendLine(role == "user" ? $"клиент: {text}" : $"агент: {text}");
- Можно заменить "клиент" и "агент" на что-то другое, в зависимости от стиля общения
---
- Запускаем проект
- пишем боту /start
- общаемся

---

# How to use the bot?
- Copying the repository
- Go to appsettings.json
- Changing the bot token and API key
- Open the Services/TogetherService.cs file
- Find the var payload, inside it model, message, etc. - these are the model parameters that can be changed
- model — choose another model
- temperature — creativity of responses (0.2 — strict responses, 1.0 — creative)
- max_tokens — maximum response length
---
- Open the file Services/HistoryService.cs and find the method:
- sb.AppendLine ("Enter the mood boost"); - this is the mood boost, it can be changed to any other
- You can also change the signatures in the message history:
- sb.AppendLine(role == "user" ? $"client: {text}" : $"agent: {text}");
- You can replace "client" and "agent" with something else, depending on the communication style.
---
- Launching the project
- write to the bot / start
- we are communicating
---
