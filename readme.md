## Прокси сервер для обхода Cors с фронта

### В текущей реализации поддерживаются только Get запросы


Деплой

```
docker build -t proxy-cors .
docker run -d -p 322:322 proxy-cors
```