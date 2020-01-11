using System.Collections.Generic;

namespace DM.Helper
{
    public class ExeptionsData
    {
        private static ExeptionsData instance;
        private static List<RecomendationsStruct> _error_311_RU;
        private static List<RecomendationsStruct> _error_715_RU;
        private static List<RecomendationsStruct> _error_710_RU;
        private static List<RecomendationsStruct> _error_705_RU;
        private static List<RecomendationsStruct> _error_503_RU;
        private static List<RecomendationsStruct> _error_319_RU;
        private static List<RecomendationsStruct> _error_307_RU;

        private static List<RecomendationsStruct> _error_311_EN;
        private static List<RecomendationsStruct> _error_715_EN;
        private static List<RecomendationsStruct> _error_710_EN;
        private static List<RecomendationsStruct> _error_705_EN;
        private static List<RecomendationsStruct> _error_503_EN;
        private static List<RecomendationsStruct> _error_319_EN;
        private static List<RecomendationsStruct> _error_307_EN;

        private ExeptionsData()
        {
            setList_error_311_RU();
            setList_error_715_RU();
            setList_error_710_RU();
            setList_error_705_RU();
            setList_error_503_RU();
            setList_error_319_RU();
            setList_error_307_RU();

            setList_error_311_EN();
            setList_error_715_EN();
            setList_error_710_EN();
            setList_error_705_EN();
            setList_error_503_EN();
            setList_error_319_EN();
            setList_error_307_EN();
        }

        public static ExeptionsData getInstance()
        {
            if (instance == null)
                instance = new ExeptionsData();
            return instance;
        }

        #region get; set; for _error_311

        public List<RecomendationsStruct> GetList_error_311_RU()
        {
            return _error_311_RU;
        }

        // Заполнение коллекции для ошибки 311
        private void setList_error_311_RU()
        {
            _error_311_RU = new List<RecomendationsStruct>();

            _error_311_RU.Add(new RecomendationsStruct("Настройки OCS верны?", "Настроить OSC", "OCS", ""));
            _error_311_RU.Add(new RecomendationsStruct("Слишком высокая температура смеси?",
                "Снизить температуру смеси в горячем смесителе или время пребывания в горячем смесителе",
                "Температура смеси в горячем смесителе", ""));
            _error_311_RU.Add(new RecomendationsStruct("Неправильная форма расплава",
                "Расплав должен вытекать наружу, подогнать контризгиб. Следить за профилем", "Форма расплава", ""));
            _error_311_RU.Add(new RecomendationsStruct("Выглядит блестяще-черным / красно-коричневым?",
                "Снижать скорость, пока пленка не станет чистой." + "\n" + "Увеличить стабализатор не больше максимального допустимого",
                "Некачественное сырье в кнетере", ""));
            _error_311_RU.Add(new RecomendationsStruct("Точки выглядят матово-черными?",
                "Остановить линию! Почистить нож, головку и корпус кнетера", "Визуальное определение", ""));
            _error_311_RU.Add(new RecomendationsStruct("Черные точки по краям?",
                "Остановить линию! Почистить щечки и запорные кольца", "Визуальное определение", ""));
            _error_311_RU.Add(new RecomendationsStruct("Черные точки в измерениях",
                "Остановить линию! Почистить щечки и запорные кольца", "Данные с черных точек", ""));
        }

        public List<RecomendationsStruct> GetList_error_311_EN()
        {
            return _error_311_EN;
        }

        // Заполнение коллекции для ошибки 311
        private void setList_error_311_EN()
        {
            _error_311_EN = new List<RecomendationsStruct>();

            _error_311_EN.Add(new RecomendationsStruct("OCS settings are correct?", "Configure OSC", "OCS", ""));
            _error_311_EN.Add(new RecomendationsStruct("Is the mixture temperature too high?",
            "Reduce the temperature of the mixture in a hot mixer or residence time in a hot mixer",
            "Mixing temperature in a hot mixer", ""));
            _error_311_EN.Add(new RecomendationsStruct("Invalid Melt Form",
            "The melt must flow outward, adjust the counter-flex. Follow the profile "," Melt shape "," "));
            
            _error_311_EN.Add(new RecomendationsStruct("Looks brilliant-black / red-brown?",
            "Reduce the speed until the film is clean." + "\n" + "Increase the stabilizer is no more than the maximum allowable",
            "Poor quality raw materials in the ketter", ""));
            _error_311_EN.Add(new RecomendationsStruct("Are the dots look matte black?",
            "Stop the line! Clean the knife, the head and body of the knot", "Visual definition", ""));
            _error_311_EN.Add(new RecomendationsStruct("Black dots on the edges?",
            "Stop the line! Clean the cheeks and locking rings", "Visual definition", ""));
            _error_311_EN.Add(new RecomendationsStruct("Black dots in dimensions",
            "Stop the line! Clean the cheeks and locking rings", "Data from black dots", ""));
        }

        #endregion

        #region get; set; for _error_715

        public List<RecomendationsStruct> GetList_error_715_RU()
        {
            return _error_715_RU;
        }

        // Заполнение коллекции для ошибки 715
        private void setList_error_715_RU()
        {
            _error_715_RU = new List<RecomendationsStruct>();

            _error_715_RU.Add(new RecomendationsStruct("Большая вытяжка, начиная с первого съемного вала?",
                "Уменьшить вытяжку, сократив разность скоростей валов." + "\n" + "Прижимной вал прижать, чтобы не допустить протаскивания пленки на намотку",
                "Скорость 1-2 валков", "OPC_ABZ12_XV_K02"));
            _error_715_RU.Add(new RecomendationsStruct("Налипание на вал 4 из-за высокой температуры",
                "Уменьшить температуру на валах 3 и 4",
                "Температура на валах 3 и 4", "OPC_ABZ36_XT_K02"));
            _error_715_RU.Add(new RecomendationsStruct("Налипание на вал 4 из-за нестандартной рецептуры",
                "Перейти на стандартную рецептуру", "Рецептура", ""));
            _error_715_RU.Add(new RecomendationsStruct("Скорость посткаландра в норме?",
                "Установить нормальные режими каландрования",
                "Скорость 3-6 валков", "OPC_ABZ36_XV_K02"));
            _error_715_RU.Add(new RecomendationsStruct("Температура посткаландра в норме?",
               "Установить нормальные режими каландрования",
               "Температура 1-2 валков", "OPC_ABZ12_XT_K02"));
        }


        public List<RecomendationsStruct> GetList_error_715_EN()
        {
            return _error_715_EN;
        }

        // Заполнение коллекции для ошибки 715
        private void setList_error_715_EN()
        {
            _error_715_EN = new List<RecomendationsStruct>();

            _error_715_EN.Add(new RecomendationsStruct("Large exhaust, starting with the first removable shaft?",
            "Reduce the hood by reducing the difference in shaft speeds." + "\n" + "Press the pressure roller to prevent the film from being dragged onto the winding",
            "Speed ​​of 1-2 rolls", "OPC_ABZ12_XV_K02"));
            _error_715_EN.Add(new RecomendationsStruct("Sticking on shaft 4 due to high temperature",
            "Reduce the temperature on shafts 3 and 4",
            "Temperature on shafts 3 and 4", "OPC_ABZ36_XT_K02"));
            _error_715_EN.Add(new RecomendationsStruct("Sticking to shaft 4 due to non-standard recipe",
            "Go to the standard recipe", "Recipe", ""));
            _error_715_EN.Add(new RecomendationsStruct("Post-liner speed is normal?",
            "Set normal calendering modes",
            "Speed ​​3-6 rolls", "OPC_ABZ36_XV_K02"));
            _error_715_EN.Add(new RecomendationsStruct("Postalandra temperature is normal?",
            "Set normal calendering modes",
            "Temperature of 1-2 rollers", "OPC_ABZ12_XT_K02"));
        }
        #endregion

        #region get; set; for _error_710

        public List<RecomendationsStruct> GetList_error_710_RU()
        {
            return _error_710_RU;
        }

        // Заполнение коллекции для ошибки 710
        private void setList_error_710_RU()
        {
            _error_710_RU = new List<RecomendationsStruct>();

            _error_710_RU.Add(new RecomendationsStruct("Несоответствующая температура на 3-4 валах?",
                "Откорректировать температуру на 3-4 валах каландра",
                "Температура 3-4 валков", "-"));
            _error_710_RU.Add(new RecomendationsStruct("Несоответствующая вытяжка?",
                "Привести вытяжку в норму",
                "Вытяжка", "-"));
            _error_710_RU.Add(new RecomendationsStruct("Скорость посткаландра в норме?",
                "Уменьшить скорость",
                "Скорость посткаландра", "-"));
            _error_710_RU.Add(new RecomendationsStruct("Ошибка в рецептуре?",
               "Скорректировать рецептуру. Поменять мешок с модификатором.",
               "Рецептура", "-"));
        }


        public List<RecomendationsStruct> GetList_error_710_EN()
        {
            return _error_710_EN;
        }

        // Заполнение коллекции для ошибки 710
        private void setList_error_710_EN()
        {
            _error_710_EN = new List<RecomendationsStruct>();

            _error_710_EN.Add(new RecomendationsStruct("Inadequate temperature on 3-4 shafts?",
            "Correct the temperature on 3-4 rolls of the calender",
            "Temperature of 3-4 rolls", "-"));
            _error_710_EN.Add(new RecomendationsStruct("Inappropriate hood?",
            "Bring the hood back to normal",
            "Extract", "-"));
            _error_710_EN.Add(new RecomendationsStruct("Post-normal speed is normal?",
            "Decrease speed",
            "Post-liner speed", "-"));
            _error_710_EN.Add(new RecomendationsStruct("Error in the recipe?",
            "Adjust recipe .To change sack with modifier.",
            "Recipe", "-"));
        }

        #endregion

        #region get; set; for _error_705

        public List<RecomendationsStruct> GetList_error_705_RU()
        {
            return _error_705_RU;
        }

        // Заполнение коллекции для ошибки 710
        private void setList_error_705_RU()
        {
            _error_705_RU = new List<RecomendationsStruct>();

            _error_705_RU.Add(new RecomendationsStruct("Скорость линии большая?",
                "Уменьшить скорость линии. Увеличить силу натяжения полиэтилена.",
                "Скорость линии ???", "-"));
            _error_705_RU.Add(new RecomendationsStruct("Низкая температура на кашировальных валах?",
                "Уменьшить скорость линии." + "\n" + "Поднять температуру на валах для каширования.",
                "Скорость линии ???", "-"));
            _error_705_RU.Add(new RecomendationsStruct("Малое усилие прижима полиэтилена?",
                "Устранить неполадки в механизме прижима. Увеличить силу натяжения потиэтилена.",
                "Механизм прижима ???", "-"));
            _error_705_RU.Add(new RecomendationsStruct("Некачественный клей на полиэтилене?",
               "Заменить рулон полиэтилена.",
               "Клей", "-"));
        }


        public List<RecomendationsStruct> GetList_error_705_EN()
        {
            return _error_705_EN;
        }

        // Заполнение коллекции для ошибки 710
        private void setList_error_705_EN()
        {
            _error_705_EN = new List<RecomendationsStruct>();

            _error_705_EN.Add(new RecomendationsStruct("Line speed is large?",
            "Reduce the speed of the line. Increase the tension of the polyethylene. ",            
            "Line speed ???", "-"));
            _error_705_EN.Add(new RecomendationsStruct("Low temperature on the laminating rolls?",
            "Reduce the line speed." + "\n" + "Raise the temperature on the shafts for laminating.",
            "Line speed ???", "-"));
            _error_705_EN.Add(new RecomendationsStruct("Low pressing force of polyethylene?",
            "Troubleshoot the clamping mechanism. Increase the tensile strength of potyethylene. ",            
            "The clamping mechanism ???", "-"));
            _error_705_EN.Add(new RecomendationsStruct("Low-quality glue on polyethylene?",
            "Replace the roll of polyethylene.",
            "Glue", "-"));
        }

        #endregion

        #region get; set; for _error_503

        public List<RecomendationsStruct> GetList_error_503_RU()
        {
            return _error_503_RU;
        }

        // Заполнение коллекции для ошибки 710
        private void setList_error_503_RU()
        {
            _error_503_RU = new List<RecomendationsStruct>();

            _error_503_RU.Add(new RecomendationsStruct("Контризгиб установлен правильно?",
                "Откорректировать контризгиб 4-го валка.",
                "Контризгиб 4-го валка", "-"));
            _error_503_RU.Add(new RecomendationsStruct("Усадка меньше допустимой?",
                "Добавить скорость на первом съемном если усадка позволяет.",
                "Усадка", "-"));

        }

        public List<RecomendationsStruct> GetList_error_503_EN()
        {
            return _error_503_EN;
        }

        // Заполнение коллекции для ошибки 710
        private void setList_error_503_EN()
        {
            _error_503_EN = new List<RecomendationsStruct>();

            _error_503_EN.Add(new RecomendationsStruct("Is the countergraph installed correctly?",
             "Correct the counter-flex of the 4th roll.",
             "Counter-flex of the 4th roll", "-"));
            _error_503_EN.Add(new RecomendationsStruct("Shrinkage is less than permissible?",
            "Add speed to the first removable if shrinkage allows.",
            "Shrinkage", "-"));

        }

        #endregion

        #region get; set; for _error_319

        public List<RecomendationsStruct> GetList_error_319_RU()
        {
            return _error_319_RU;
        }

        // Заполнение коллекции для ошибки 319
        private void setList_error_319_RU()
        {
            _error_319_RU = new List<RecomendationsStruct>();

            _error_319_RU.Add(new RecomendationsStruct("Каландровые валки слишком горячие?",
                "Снизить температуру каландровых валков.",
                "Температура каландровых валков.", "-"));
            _error_319_RU.Add(new RecomendationsStruct("Масса в кнетере перегрета?",
                "Убавить температуру в пластикаторе.",
                "Температура в пластикаторе", "-"));
            _error_319_RU.Add(new RecomendationsStruct("Слишком мало термостабилизатора?",
                "Увеличить содержание термостабилизатора (можно до 1.1%)",
                "Содержание термостабилизатора", "-"));
            _error_319_RU.Add(new RecomendationsStruct("Кнет слишком толстый?",
                "Уменьшить кнет и следить за ним.",
                "Размер кнета", "-"));
            _error_319_RU.Add(new RecomendationsStruct("Фрикция на валках каландра слишком мала?",
                "Увеличить фрикцию и выставить кнет.",
                "Фрикция", "-"));
            _error_319_RU.Add(new RecomendationsStruct("Кнет слишком толстый?",
                "Уменьшить кнет и следить за ним.",
                "Размер кнета", "-"));
        }


        public List<RecomendationsStruct> GetList_error_319_EN()
        {
            return _error_319_EN;
        }

        // Заполнение коллекции для ошибки 319
        private void setList_error_319_EN()
        {
            _error_319_EN = new List<RecomendationsStruct>();

            _error_319_EN.Add(new RecomendationsStruct("Calender rolls too hot?",
            "Reduce the temperature of the calender rolls.",
            "Temperature of calender rollers.", "-"));
            _error_319_EN.Add(new RecomendationsStruct("The mass in the ketter is overheated?",
            "Reduce the temperature in the plasticizer.",
            "Temperature in the plasticizer", "-"));
            _error_319_EN.Add(new RecomendationsStruct("Too little heat stabilizer?",
            "Increase the content of the thermal stabilizer (up to 1.1%)",
            "Content of thermal stabilizer", "-"));
            _error_319_EN.Add(new RecomendationsStruct("Knut too thick?",
            "Reduce the whip and watch it.",
            "Size of the knight", "-"));
            _error_319_EN.Add(new RecomendationsStruct("The friction on the rollers of the calender is too small?",
            "Increase the friction and expose the kneet.",
            "Friction", "-"));
            _error_319_EN.Add(new RecomendationsStruct("Knut too thick?",
            "Reduce the whip and watch it.",
            "Size of the knight", "-"));
        }

        #endregion

        #region get; set; for _error_307

        public List<RecomendationsStruct> GetList_error_307_RU()
        {
            return _error_307_RU;
        }

        // Заполнение коллекции для ошибки 319
        private void setList_error_307_RU()
        {
            _error_307_RU = new List<RecomendationsStruct>();

            _error_307_RU.Add(new RecomendationsStruct("Вакуум на кнетере менее 0.6?",
                "Перевести вакуумную линию на второй бак." + "\n" + "Грязный бак очистить и проверить на герметичность",
                "Температура каландровых валков.", "-"));
            _error_307_RU.Add(new RecomendationsStruct("Дегазационный валок поврежден?",
                "При необходимости остановить линию для устранения повреждения.",
                "Дегазационный валок", "-"));
            _error_307_RU.Add(new RecomendationsStruct("Много запаса материала в кнете?",
                "Уменьшить запас материала в кнете",
                "Количество материала в кнете", "-"));
            _error_307_RU.Add(new RecomendationsStruct("Расплав плохо вращается в зазоре?",
                "Температуру валков между 1 и 2 увеличить",
                "Температура 1 и 2 валков", "-"));
            _error_307_RU.Add(new RecomendationsStruct("Слишком большое соотношение вытяжки и фрикций?",
                "Уменьшить фрикцию, вытяжку и зазор между валками",
                "Фрикция" + "\n" + "Вытяжку" + "\n" + "Зазор", "-"));
            _error_307_RU.Add(new RecomendationsStruct("Валы 1-4 слишком горячие?",
                "Уменьшить температуру, с целью снижения скорости вытяжных валков для получения тонкой шубы на валках.",
                "Температура", "-"));
            _error_307_RU.Add(new RecomendationsStruct("Стандартная рецептура?",
                "Перейти на стандартную рецептуру",
                "Рецептура", "-"));

        }

        public List<RecomendationsStruct> GetList_error_307_EN()
        {
            return _error_307_EN;
        }

        // Заполнение коллекции для ошибки 319
        private void setList_error_307_EN()
        {
            _error_307_EN = new List<RecomendationsStruct>();

            _error_307_EN.Add(new RecomendationsStruct("Vacuum on the Kneeter is less than 0.6?",
            "Translate the vacuum line to the second tank." + "\n" + "Clean the dirty tank and check for leaks",
            "Temperature of calender rollers.", "-"));
            _error_307_EN.Add(new RecomendationsStruct("Degassing roll is damaged?",
            "If necessary, stop the line to repair the damage.",
            "Degassing roll", "-"));
            _error_307_EN.Add(new RecomendationsStruct("A lot of stock in the book?",
            "Reduce the stock of material in the knout",
            "The amount of material in the knight", "-"));
            _error_307_EN.Add(new RecomendationsStruct("Does the melt rotate poorly in the gap?",
            "The temperature of the rollers between 1 and 2 increase",            
            "Temperature of 1 and 2 rollers", "-"));
            _error_307_EN.Add(new RecomendationsStruct("Too much ratio of hoods and frictions?",
            "Reduce the friction, stretching and gap between the rolls",
            "Friction" + "\n" + "Extension" + "\n" + "Clearance","-"));
            
            _error_307_EN.Add(new RecomendationsStruct("Shafts 1-4 too hot?",
            "Reduce the temperature, in order to reduce the speed of the exhaust rolls to obtain a thin coat on the rolls.",
            "Temperature", "-"));
            _error_307_EN.Add(new RecomendationsStruct("Standard recipe?",
            "Go to the standard recipe",
            "Recipe", "-"));

        }

        #endregion
    }
}