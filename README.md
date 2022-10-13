# Создание записей для сменного расписания
Database projects // SGS Vostok Limited

**DatabaseProject_SGS_VostokLimited/WPF/WPFworkSchedule/bin/Release/WPFworkSchedule.exe**<br>
Приложение <code>WPFworkSchedule.exe</code> представляет из себя форму для создания .json-файлов. При сохранении происходит промпт на сохранение нового файла от Windows с предвыбранными форматом и именем. Формат json-файла:
```
{
  "Город": "",
  "Цех": "",
  "Сотрудник": "",
  "Бригада": "",
  "Смена": ""
}
```

Интерфейс формы:<br>
<img src='https://files.catbox.moe/qfj3ph.png'>



**DatabaseProject_SGS_VostokLimited/JS/index.html** (+scripts.js)<br>
Веб-форма index.html имеет тот же интерфейс с теми же полями (добавлена кнопка сброса полей).
При сохранении (нажатии по кнопке "Сохранить...") в консоли DevTools можно увидеть уведомление о новой записи SQL базы данных. Можно сохранять больше 1 записи.

Интерфейс сайта + devTools:<br>
<img src='https://files.catbox.moe/2c1m3p.png'>



Можно попробовать повторить на ресурсе https://jsfiddle.net, но я не стал.<br>
Если необходимо просто написать скрипт по созданию таблицы на MS SQL для данных полей, то на ресурсе https://sqliteonline.com это делается командой:
```
CREATE TABLE shiftEntry (Город TEXT, Цех TEXT, Сотрудник TEXT, Бригада TEXT, Смена TEXT)
```
Скрипт для добавления и вывода записей:
```
INSERT INTO shiftEntry (Город, Цех, Сотрудник, Бригада, Смена) VALUES ('Moscow', 'MoscowCity Department', 'Ivanov I.I.', 'Group 8:00-20:00', 'First shift')
SELECT * FROM shiftEntry
```
