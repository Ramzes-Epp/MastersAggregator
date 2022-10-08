Проект MastersAggregator (задумывался как микросервис взаимодействие с другими сервисами через Web api), 
- бэкенд: dotnet core, БД - PostgreSQL, ORM - Dapper 
- фронтенд: AvaloniaUI (кроссплатформенный фреймворк), XAML, C# также добавлен Swagger.

Приложение MastersAggregator предназначено для диспетчеризации в коммунальном хозяйстве - распределение заказов на ремонтные работы среди мастеров.

Идея достаточно простая: к диспетчеру приходят заказы на выполнения работ от жильцов дома, он распределяет заказы среди мастеров.

Типы пользователей системы:

Диспетчер - будет распределять заказы мастерам.
Юзер - будет оставлять заявки с описанием работы.
Мастер - будет выполнять заказ.

Функционал:

возможность логиниться по ApiKey
возможность под Диспетчер редактировать все данные.
возможность Мастеров открывать список своих заказов.
возможность юзером редактировать, удалять свои заказы. 

Вторая версия будет включать в себя возможность юзера самостоятельно выбрать мастера.
В финале видится система куда юзер может зайти, подать заявку на выполнение работы, выбрать мастера и дату и время когда мастер сможет выполнить работу. 
Также планируется добавить систему отзывов для мастеров.

Фронтенд на AvaloniaUI:
![alt text](https://github.com/Ramzes-Epp/MastersAggregator/blob/master/MasterAggregator.Desktop/Assets/Foto%20frontend/3.jpg?raw=true)

![alt text](https://github.com/Ramzes-Epp/MastersAggregator/blob/master/MasterAggregator.Desktop/Assets/Foto%20frontend/5.jpg?raw=true)

![alt text](https://github.com/Ramzes-Epp/MastersAggregator/blob/master/MasterAggregator.Desktop/Assets/Foto%20frontend/4.jpg?raw=true)

![alt text](https://github.com/Ramzes-Epp/MastersAggregator/blob/master/MasterAggregator.Desktop/Assets/Foto%20frontend/2.jpg?raw=true)

Фронтенд на Swagger:
![alt text](https://github.com/Ramzes-Epp/MastersAggregator/blob/master/MasterAggregator.Desktop/Assets/Foto%20frontend/6.jpg?raw=true)

![alt text](https://github.com/Ramzes-Epp/MastersAggregator/blob/master/MasterAggregator.Desktop/Assets/Foto%20frontend/7.jpg?raw=true)
