var cSel, dSel, wSel, g1Rb, g2Rb, sSel, sBut;

var db;

const arrDropdownFill = [
  ['Москва', 'ц.М-А', 'Анатолий'], ['Москва', 'ц.М-Г', 'Фёдор'],
  ['Москва', 'ц.М-Б', 'Александр'], ['Москва', 'ц.М-Г', 'Лев'],
  ['Москва', 'ц.М-В', 'София'], ['Москва', 'ц.М-ДЕ', 'Ульяна'],
  ['Санкт-Петербург', 'ц.СП-А', 'Мария'], ['Санкт-Петербург', 'ц.СП-В', 'Вера'],
  ['Санкт-Петербург', 'ц.СП-А', 'Максим'], ['Санкт-Петербург', 'ц.СП-Г', 'Николай'],
  ['Санкт-Петербург', 'ц.СП-Б', 'Михаил'], ['Санкт-Петербург', 'ц.СП-Д', 'Владислав'],
  ['Санкт-Петербург', 'ц.СП-В', 'Артём'], ['Санкт-Петербург', 'ц.СП-Ц', 'Арсений'],
  ['Новосибирск', 'ц.Н-А', 'Даниил'], ['Новосибирск', 'ц.Н-А', 'Тимур'],
  ['Новосибирск', 'ц.Н-А', 'Анна'], ['Новосибирск', 'ц.Н-Б', 'Алёна'],
  ['Новосибирск', 'ц.Н-А', 'Иван'], ['Новосибирск', 'ц.Н-В', 'Мирон'],
  ['Омск', 'ц.О-А', 'Александра'], ['Омск', 'ц.О-П', 'Юлия'],
  ['Омск', 'ц.О-Б', 'Дарья'], ['Омск', 'ц.О-П', 'Диана'],
  ['Омск', 'ц.О-В', 'Данил'], ['Омск', 'ц.О-Я0', 'Виктор'],
  ['Омск', 'ц.О-Г', 'Екатерина'], ['Омск', 'ц.О-Я1', 'Олег'],
  ['Омск', 'ц.О-Д', 'Матвей'], ['Омск', 'ц.О-Я2', 'Богдан'],
  ['Самара', 'ц.С-А', 'Ксения'], ['Самара', 'ц.С-Б', 'Мирослава'],
  ['Самара', 'ц.С-Б', 'Арина'], ['Самара', 'ц.С-Ж', 'Демид'],
  ['Пермь', 'ц.П-А', 'Егор'], ['Пермь', 'ц.П-ГД', 'Есения'],
  ['Пермь', 'ц.П-А', 'Ева'], ['Пермь', 'ц.П-Е', 'Антон'],
  ['Пермь', 'ц.П-Б', 'Илья'], ['Пермь', 'ц.П-У', 'Злата'],
  ['Пермь', 'ц.П-В', 'Тимофей'], ['Пермь', 'ц.П-С', 'Майя'],
  ['Пермь', 'ц.П-В', 'Василиса'], ['Пермь', 'ц.П-Т', 'Ника'],
];

// alert('Alert from global JS file');

function initialize() {
  cSel = document.getElementById('cityID');
  dSel = document.getElementById('depID');
  wSel = document.getElementById('workerID');
  g1Rb = document.getElementById('groupFirstID');
  g2Rb = document.getElementById('groupSecondID');
  sSel = document.getElementById('shiftID');
  sBut = document.getElementById('saveID');

  db = window.openDatabase('data', '1.0', 'data', 1 * 1024 * 1024);
  db.transaction(t => {
    t.executeSql('CREATE TABLE shiftEntry02 (Город TEXT, Цех TEXT, Сотрудник TEXT, Бригада TEXT, Смена TEXT)');
  }, e => console.error(e));

  var citList = [];
  for (var arr of arrDropdownFill) {
    citList.push(arr[0]);
    citList = citList.filter(onlyUnique);
  }

  for (var i = 0; i < citList.length; i++) {
    var opt = citList[i];
    var el = document.createElement('option');
    el.textContent = opt;
    el.value = opt;
    cSel.appendChild(el);
  }

  resetSelection(cSel);
  disableInput(dSel);
  disableInput(wSel);
  disableInput(g1Rb);
  disableInput(g2Rb);
  disableInput(sSel);
  disableInput(sBut);
}

function cityChanged() {
  resetSelection(dSel);
  resetSelection(wSel);
  resetSelection(sSel);
  enableInput(dSel);
  disableInput(wSel);
  disableInput(g1Rb);
  disableInput(g2Rb);
  disableInput(sSel);
  disableInput(sBut);

  dSel.innerHTML = '';

  var depList = [];
  for (var arr of arrDropdownFill) {
    if (arr[0] == cSel.options[cSel.selectedIndex].text.trim()) {
      depList.push(arr[1]);
      depList = depList.filter(onlyUnique);
    }
  }

  for (var i = 0; i < depList.length; i++) {
    var opt = depList[i];
    var el = document.createElement('option');
    el.textContent = opt;
    el.value = opt;
    dSel.appendChild(el);
  }

  resetSelection(dSel);
}

function departmentChanged() {
  resetSelection(wSel);
  resetSelection(sSel);
  enableInput(wSel);
  disableInput(g1Rb);
  disableInput(g2Rb);
  disableInput(sSel);
  disableInput(sBut);

  wSel.innerHTML = '';

  var worList = [];
  for (var arr of arrDropdownFill) {
    if (arr[1] == dSel.options[dSel.selectedIndex].text.trim()) {
      worList.push(arr[2]);
      worList = worList.filter(onlyUnique);
    }
  }

  for (var i = 0; i < worList.length; i++) {
    var opt = worList[i];
    var el = document.createElement('option');
    el.textContent = opt;
    el.value = opt;
    wSel.appendChild(el);
  }

  resetSelection(wSel);
}

function workerChanged() {
  enableInput(g1Rb);
  enableInput(g2Rb);
  enableInput(sSel);
  resetSelection(sSel);
}

function groupChanged() {
  // enableInput(sSel);
}

function shiftChanged() {
  enableInput(sBut);
}

function saveClicked() {
  if (!g1Rb.checked && !g2Rb.checked) {
    alert('Заполните поле "Бригада"!');
  }
  else {
    dbCreatePopulate();
  }
}


function dbCreatePopulate() {
  var rbSel;
  if (g1Rb.checked) {
    rbSel = 'Дневная бригада (с 8:00 до 20:00)';
  }

  if (g2Rb.checked) {
    rbSel = 'Ночная бригада (с 20:00 до 8:00)';
  }

  const jsonFile = [{
    city: cSel.options[cSel.selectedIndex].text.trim(),
    department: dSel.options[dSel.selectedIndex].text.trim(),
    worker: wSel.options[wSel.selectedIndex].text.trim(),
    subgroup: rbSel,
    shift: sSel.options[sSel.selectedIndex].text.trim(),
  },];

  db.transaction(t => {
    for (let g of jsonFile) {
      t.executeSql('INSERT INTO shiftEntry02 (Город, Цех, Сотрудник, Бригада, Смена) VALUES (?, ?, ?, ?, ?)',
        [g.city, g.department, g.worker, g.subgroup, g.shift]);
    }
  }, e => console.error(e));

  db.transaction(t => t.executeSql(
    'SELECT * FROM shiftEntry02 g', [],
    (t, result) => console.log(result.rows)
  ));

  alert('Alert from dbCreatePopulate');
}


function clearClicked() {
  initialize();
}

function onlyUnique(value, index, self) {
  return self.indexOf(value) === index;
}

function resetSelection(elemRef) {
  elemRef.selectedIndex = -1;
}

function disableInput(elemRef) {
  elemRef.disabled = true;
}

function enableInput(elemRef) {
  elemRef.disabled = false;
}
