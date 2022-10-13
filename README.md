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
При сохранении (нажатии по кнопке "Сохранить..." в консоли DevTools можно увидеть уведомление о новой записи SQL базы данных. Можно сохранять больше 1 записи.

Интерфейс сайта + devTools:<br>
<img src='https://files.catbox.moe/2c1m3p.png'>