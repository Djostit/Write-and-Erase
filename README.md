




## Использование программы 🔥
Для начала вам нужно указать в файле `appsetting.json` данные для вашей бд
```
{
  "ConnectionStrings": {
    "DefaultConnection": "server=[your_server];user=[your_user];password=[your_password];database=[your_database];",
  }
}
```

[Скачать](https://github.com/Djostit/Write-and-Erase/releases)





# ООО «Пиши-стирай» - магазин по продаже канцелярских товаров. 📎
В рамках выполнения задания демонстрационного экзамена необходимо разработать основные модули информационной системы для ООО «Пиши-стирай»:
- Неавторизованный клиент и авторизованный клиент может просматривать товары и формировать заказ;
- Менеджер может просматривать товары, формировать и редактировать заказы;
- Администратор может добавлять/редактировать/удалять товары, просматривать и редактировать
заказы

Кроме того, разрабатываемая Вами информационная система предполагает установку на терминалах при входе в торговые центры города. На терминале клиент (авторизованный и неавторизованный) может просмотреть товары, сформировать заказ и выбрать удобный для него пункт выдачи.

## Рудоводство по стилю 📋
- Общие требования
  - При создании приложения руководствуйтесь требованиями, описанными в документе «Требования и рекомендации.pdf». Не допускайте орфографические и грамматические ошибки.
- Использование логотипа
  - Все экранные формы пользовательского интерфейса должны иметь заголовок с логотипом (в ресурсах). Не искажайте логотип (не изменяйте изображение, его пропорции, цвет).
  - Также для приложений должна быть установлена иконка.
- Шрифт
  - Используйте шрифт Comic Sans MS
- Цветовая схема
  - В качестве основого фона используется `#ffffff`
  - В качестве дополнительного используется `#76e383`
  - Для акцентирования внимания пользователя на целевое действие интерфейса используется `#498c51`
## Требования и рекомендации📝
### Введение 🗂️
Настоящий документ определяет правила выполнения экзаменационного задания для компетенции «Программные решения для бизнеса». Для выполнения задач экзаменационного задания вы можете использовать любые инструменты, предоставляемые согласно инфраструктурного листа.
В случае нехватки времени для выполнения всех оставшихся задач вы можете пропускать выполнение некоторых задач в пользу других. Однако ожидается, что вы предоставите максимально завершенную работу в конце каждой сессии, чтобы облегчить оценку вашей работы.
### Правила 📄
- Во время проведения экзамена необходимо соблюдать следующие правила:
- Запрещен доступ в Интернет (кроме разового доступа в течение сессии не более 15 минут);
- Запрещено использование любых гаджетов (мобильный телефон, планшет, смарт-часы и т.д.);
- Запрещено использование ваших собственных устройств хранения данных (USB-накопители, жесткие диски и т.д.);
- Запрещено общение с другими участниками экзамена;
- Запрещено приносить на экзамен книги, заметки и т.д.;
- Разрешено использовать личные устройства ввода информации (клавиатура, мышь, трекбол и т.д.), но эти устройства должны быть проводными, непрограммируемыми и должны работать без дополнительной установки драйверов (эти требования предварительно проверяются техническим экспертом);
- Разрешено использовать личные средства повышения эргономики (коврик для мыши, подставка под запястья и т.д.), а также талисманы (также проходят проверку у технического эксперта);
- При возникновении любой внештатной ситуации с программным или аппаратным обеспечением, а также периферийными устройствами необходимо немедленно прервать работу и обратиться к эксперту.

Несоблюдение этих правил может привести к удалению с площадки проведения экзамена.

### Название приложения 🏷️
Используйте соответствующие названия для ваших приложений и файлов. Так, например, наименование настольного приложения должно обязательно включать название компании заказчика.
### Файловая структура 🗄️
Файловая структура проекта должна отражать логику, заложенную в приложение. Например, все формы содержатся в одной директории,
пользовательские визуальные компоненты – в другой, классы сущностей – в третьей.
### Структура проекта 📑
Каждая сущность должна быть представлена в программе как минимум одним отдельным классом. Классы должны быть небольшими, понятными и выполнять одну единственную функцию (Single responsibility principle). Для работы с разными сущностями используйте разные формы, где это уместно.
### Логическая структура 🗃️
Логика представления (работа с пользовательским вводом/выводом, формы, обработка событий) не должна быть перемешана с бизнеслогикой (ограничения и требования, сформулированные в заданиях), а также не должна быть перемешана с логикой доступа к базе данных (SQL-запросы, запись, получение данных). В идеале это должны быть три независимых модуля.

### Руководство по стилю 📈
Визуальные компоненты должны соответствовать руководству по стилю, предоставленному в качестве ресурсов к заданию в соответствующем файле. Обеспечьте соблюдение требований всех компонентов в следующих областях:
- цветовая схема,
- размещение логотипа,
- использование шрифтов,
- установка иконки приложения.
### Макет и технические характеристики 📊
Все компоненты системы должны иметь единый согласованный внешний вид, соответствующий руководству по стилю, а также следующим требованиям:
- разметка и дизайн (предпочтение отдается масштабируемой компоновке;
- должно присутствовать ограничение на минимальный размер окна;
- должна присутствовать возможность изменения размеров окна, где это необходимо;
- увеличение размеров окна должно увеличивать размер контентной части, например, таблицы с данными из БД);
- группировка элементов (в логические категории);
- использование соответствующих элементов управления (например, выпадающих списков для отображения подстановочных значений из базы данных);
- расположение и выравнивание элементов (метки, поля для ввода и т.д.);
- последовательный переход фокуса по элементам интерфейса (по нажатию клавиши TAB);
- общая компоновка логична, понятна и проста в использовании;
- последовательный пользовательский интерфейс, позволяющий перемещаться между существующими окнами в приложении (в том числе обратно, например, с помощью кнопки «Назад»);
- соответствующий заголовок на каждом окне приложения (не должно быть значений по умолчанию типа MainWindow, Form1 и тп).

### Обратная связь с пользователем 🤚🏻
Уведомляйте пользователя о совершаемых им ошибках или о запрещенных в рамках задания действиях, запрашивайте подтверждение перед удалением, предупреждайте о неотвратимых операциях, информируйте об отсутствии результатов поиска и т.п. Окна сообщений
соответствующих типов (например, ошибка, предупреждение, информация) должны отображаться с соответствующим заголовком и пиктограммой. Текст сообщения должен быть полезным и информативным, содержать полную информацию о совершенных ошибках
пользователя и порядок действий для их исправления. Также можно использовать визуальные подсказки для пользователя при вводе данных.
### Обработка ошибок 💣
Не позволяйте пользователю вводить некорректные значения в текстовые поля сущностей. Например, в случае несоответствия типа данных или размера поля введенному значению. Оповестите пользователя о совершенной им ошибке.
Обратите внимание на использование абсолютных и относительных путей к изображениям. Приложение должно корректно работать, в том числе и при перемещении папки с исполняемым файлом.
При возникновении непредвиденной ошибки приложение не должно аварийно завершать работу
### Оформление кода 💬
Идентификаторы переменных, методов и классов должны отражать суть и/или цель их использования, в том числе и наименования элементов управления (например, не должно быть значений по умолчанию типа Form1, button3).
Идентификаторы должны соответствовать соглашению об именовании (Code Convention) и стилю CamelCase (для C# и Java) и snake_case (для Python). Допустимо использование не более одной команды в строке.

### Комментарии ⚡
Используйте комментарии для пояснения неочевидных фрагментов кода. Запрещено комментирование кода. 
Хороший код воспринимается как обычный текст. Не используйте комментарии для пояснения очевидных действий. Комментарии должны присутствовать только в местах, которые требуют дополнительного пояснения.
Используйте тип комментариев, который в дальнейшем позволит сгенерировать XML- документацию, с соответствующими тегами (например, param, return(s), summary и др.)
### Оценка 🌈
Каждая задача оценивается путем тестирования реализации требуемого функционала. Так как требования к реализуемой системе очень высоки, возможно, будут использоваться средства для автоматизированного тестирования приложения. В связи с этим, в ходе разработки, может
возникнуть необходимость следовать определенным правилам именования и структурирования проекта.

## Предоставление результатов 💼
Все практические результаты должны быть переданы заказчику путем загрузки файлов на предоставленный вам репозиторий системы контроля версий git. Практическими результатами являются:
- исходный код приложения (в виде коммита текущей версии проекта, но не архивом),
- исполняемые файлы,
- прочие графические/текстовые файлы.

Результаты работы каждой сессии должны быть загружены в отдельный репозиторий с названием «Сессия X» (X – номер сессии).

Для оценки работы будет учитываться только содержимое репозитория. При оценке рассматриваются заметки только в электронном виде (readme.md). Рукописные примечания не будут использоваться для оценки.

Проект обязательно должен содержать описание в формате Markdown (см. шаблон в файле README-Template.md или README-Template_rus.md). Заполните также дополнительную информацию о проекте и способе запуска приложения в файле readme.md.

Обратите внимание, что дополнительного времени после окончания сессии на сохранение не предусмотрено, поэтому будьте бдительны и загружайте результаты работ своевременно в рамках сессии.
