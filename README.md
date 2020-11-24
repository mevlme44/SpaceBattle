# SpaceBattle
  ## Победа - уничтожить все астероиды на уровне
  ## Проигрышь - 3 раза попасть под удар астероида
  * Asteroids
      * Asteroid.cs
         >Вспомогательный класс, описывающий поведение астероида.
      * AsteroidBoss.cs
         >Вспомогательный класс, описывающий поведение босса-астероида.
  * Bullet
      * Shot.cs
         >Вспомогательный класс, описывающий поведение пули.
  * Managers
      * CameraOptimize.cs
          > Класс, отвечающий за вычисление размеров экрана.
      * GameManager.cs
          > Класс, отвечающий за загрузку, смену и обработку уровней.
          > * data - объект, для сохранения/загрузки данных
          > * asteroids - ссылки на префабы обычных астероидов
          > * asteroidBoss - ссылка на префаб босса-астероида
          > * backgrounds  - ссылки на текстуры фона
          > * bounds - ссылки на физические границы карты
          > * currentBackground - ссылка на объект, отображающий фон
          > * listAsteroids  - список, для хранения всех астероидов на уровне
          > * Subscribe() - Метод, вызывающий ожидание событий на нажатие кнопок UI
      * RemoteManager.cs
          > Класс, отвечающий за доступ к классу GameManager.cs из 
          > побочных классов.
      * SaveData.cs
          > Класс, описывающий сущность сохраненных данных.
          > * _amount - количество астероидов на уровне
          > * _background - фон на уровне
          > * _asteroidType - Форма и размер астероидов
          > * CurrentLevel  - прогресс по уровням
      * SaveManager.cs
          > Класс, реализующий бинарную сериализацию.
  * SpaceBoat
      * PlayerController.cs
        >Класс, реализующий контроллер игрока
        > * fireRate - частота стрельбы
        > * shotSpawn - точка инициализации выстрела
        > * shot - префаб выстрела
        > * Damage() - Метод, отвечающий за получение урона персонажем
        > * Win() - Метод, вызывающийся при победе игрока
        > * KillAsteroid() - Метод, вызывающийся при попадании пули в астероид
        > * GetInput() - Метод, принимающий действия игрока
      * PlayerView.cs
        > Класс, реализующий представление игрока
        > * hp - текстовое поле с количеством жизней игрока
        > * scoreText - текстовое поле с количеством очков игрока
        > * ResultPanel - Панель, на которой выводится результат игры
        > * Result - Текстовое поле, с резултатом игры
        > * lineFirstToSecond - Изображение, которое перекрашивается, по мере прохождения уровней
        > * textSecondLevel - Тестовое поле в кнопке, перекрашивается по мере прохождения уровней
        > * menu - Панель с главным меню
        > * Colored() - Метод, окрашивающий линии, кнопки и текст в белый цвет.
      * PlayerModel.cs
        > Класс, реализующий модель игрока
        > * bound - структура, реализующая границы экрана, для игрока
        > * CalculatePosition() - Метод, расчитывающий текущую позицию, поворот и велосити игрока
        > * GetDamage() - Метод, вызывающийся при получении урона игроком
        > * KilledAsteroid() - Метод, вызывающийся при попадании пули в астероид
