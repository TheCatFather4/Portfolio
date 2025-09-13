const catPic1 = document.getElementById("catPic1");
const catPic2 = document.getElementById("catPic2");
const catPic3 = document.getElementById("catPic3");
const catPic4 = document.getElementById("catPic4");
const catPic5 = document.getElementById("catPic5");

const catName1 = document.getElementById("catName1");
const catName2 = document.getElementById("catName2");
const catName3 = document.getElementById("catName3");
const catName4 = document.getElementById("catName4");
const catName5 = document.getElementById("catName5");

const catRoll1 = document.getElementById("catRoll1");
const catRoll2 = document.getElementById("catRoll2");
const catRoll3 = document.getElementById("catRoll3");
const catRoll4 = document.getElementById("catRoll4");
const catRoll5 = document.getElementById("catRoll5");

const scoreButton = document.getElementById("scoreButton");
const rollButton = document.getElementById("rollButton");
const playAgainButton = document.getElementById("playAgainButton")

const holdButton1 = document.getElementById("holdButton1");
const holdButton2 = document.getElementById("holdButton2");
const holdButton3 = document.getElementById("holdButton3");
const holdButton4 = document.getElementById("holdButton4");
const holdButton5 = document.getElementById("holdButton5");

const btnCheck1 = document.getElementById("btn-check-outlined");
const btnCheck2 = document.getElementById("btn-check-outlined2");
const btnCheck3 = document.getElementById("btn-check-outlined3");
const btnCheck4 = document.getElementById("btn-check-outlined4");
const btnCheck5 = document.getElementById("btn-check-outlined5");

const cardBody1 = document.getElementById("cardBody1");
const cardBody2 = document.getElementById("cardBody2");
const cardBody3 = document.getElementById("cardBody3");
const cardBody4 = document.getElementById("cardBody4");
const cardBody5 = document.getElementById("cardBody5");

const remainingRollCount = document.getElementById("remainingRollCount");

const currentScore = document.getElementById("currentScore");
const highScore = document.getElementById("highScore");

let hold1 = false;
let hold2 = false;
let hold3 = false;
let hold4 = false;
let hold5 = false;

let count = Number(remainingRollCount.textContent);
let score = Number(currentScore.textContent);

function getRandomNumber() {
    let randomNumber = Math.random() * (7 - 1) + 1;
    return Math.floor(randomNumber);
}

function calculateSum(a, b, c, d, e) {
    return a + b + c + d + e;
}

function catCard1() {
    let number = getRandomNumber();
    switch (number) {
        case 1:
            catPic1.src = "img/zuko.jpg";
            catName1.textContent = "Zuko";
            cardBody1.style.background = "linear-gradient(rgb(138, 133, 74), rgb(172, 169, 142))";
            break;
        case 2:
            catPic1.src = "img/jade.jpg";
            catName1.textContent = "Jade";
            cardBody1.style.background = "linear-gradient(rgb(238, 150, 238), rgb(243, 205, 243))";
            break;
        case 3:
            catPic1.src = "img/nut.jpg";
            catName1.textContent = "Nut";
            cardBody1.style.background = "linear-gradient(rgb(236, 167, 38), rgb(240, 208, 148))";
            break;
        case 4:
            catPic1.src = "img/kitty.jpg";
            catName1.textContent = "Kitty";
            cardBody1.style.background = "linear-gradient(rgb(182, 178, 181), rgb(230, 223, 228))";
            break;
        case 5:
            catPic1.src = "img/billy.jpg";
            catName1.textContent = "Billy";
            cardBody1.style.background = "linear-gradient(rgb(38, 236, 144), rgb(162, 240, 202))";
            break;
        case 6:
            catPic1.src = "img/birdie.jpg";
            catName1.textContent = "Birdie";
            cardBody1.style.background = "linear-gradient(rgb(124, 221, 250), rgb(208, 241, 252))";
            break;
        default:
            catPic1.src = "img/placeholder.jpg";
            catName1.textContent = "Cat"
            break;
    }

    catRoll1.textContent = number;
    return number;
}

function catCard2() {
    let number = getRandomNumber();
    switch (number) {
        case 1:
            catPic2.src = "img/zuko.jpg";
            catName2.textContent = "Zuko";
            cardBody2.style.background = "linear-gradient(rgb(138, 133, 74), rgb(172, 169, 142))";
            break;
        case 2:
            catPic2.src = "img/jade.jpg";
            catName2.textContent = "Jade";
            cardBody2.style.background = "linear-gradient(rgb(238, 150, 238), rgb(243, 205, 243))";
            break;
        case 3:
            catPic2.src = "img/nut.jpg";
            catName2.textContent = "Nut";
            cardBody2.style.background = "linear-gradient(rgb(236, 167, 38), rgb(240, 208, 148))";
            break;
        case 4:
            catPic2.src = "img/kitty.jpg";
            catName2.textContent = "Kitty";
            cardBody2.style.background = "linear-gradient(rgb(182, 178, 181), rgb(230, 223, 228))";
            break;
        case 5:
            catPic2.src = "img/billy.jpg";
            catName2.textContent = "Billy";
            cardBody2.style.background = "linear-gradient(rgb(38, 236, 144), rgb(162, 240, 202))";
            break;
        case 6:
            catPic2.src = "img/birdie.jpg";
            catName2.textContent = "Birdie";
            cardBody2.style.background = "linear-gradient(rgb(124, 221, 250), rgb(208, 241, 252))";
            break;
        default:
            catPic2.src = "img/placeholder.jpg";
            catName2.textContent = "Cat"
            break;
    }

    catRoll2.textContent = number;
    return number;
}

function catCard3() {
    let number = getRandomNumber();
    switch (number) {
        case 1:
            catPic3.src = "img/zuko.jpg";
            catName3.textContent = "Zuko";
            cardBody3.style.background = "linear-gradient(rgb(138, 133, 74), rgb(172, 169, 142))";
            break;
        case 2:
            catPic3.src = "img/jade.jpg";
            catName3.textContent = "Jade";
            cardBody3.style.background = "linear-gradient(rgb(238, 150, 238), rgb(243, 205, 243))";
            break;
        case 3:
            catPic3.src = "img/nut.jpg";
            catName3.textContent = "Nut";
            cardBody3.style.background = "linear-gradient(rgb(236, 167, 38), rgb(240, 208, 148))";
            break;
        case 4:
            catPic3.src = "img/kitty.jpg";
            catName3.textContent = "Kitty";
            cardBody3.style.background = "linear-gradient(rgb(182, 178, 181), rgb(230, 223, 228))";
            break;
        case 5:
            catPic3.src = "img/billy.jpg";
            catName3.textContent = "Billy";
            cardBody3.style.background = "linear-gradient(rgb(38, 236, 144), rgb(162, 240, 202))";
            break;
        case 6:
            catPic3.src = "img/birdie.jpg";
            catName3.textContent = "Birdie";
            cardBody3.style.background = "linear-gradient(rgb(124, 221, 250), rgb(208, 241, 252))";
            break;
        default:
            catPic3.src = "img/placeholder.jpg";
            catName3.textContent = "Cat"
            break;
    }

    catRoll3.textContent = number;
    return number;
}

function catCard4() {
    let number = getRandomNumber();
    switch (number) {
        case 1:
            catPic4.src = "img/zuko.jpg";
            catName4.textContent = "Zuko";
            cardBody4.style.background = "linear-gradient(rgb(138, 133, 74), rgb(172, 169, 142))";
            break;
        case 2:
            catPic4.src = "img/jade.jpg";
            catName4.textContent = "Jade";
            cardBody4.style.background = "linear-gradient(rgb(238, 150, 238), rgb(243, 205, 243))";
            break;
        case 3:
            catPic4.src = "img/nut.jpg";
            catName4.textContent = "Nut";
            cardBody4.style.background = "linear-gradient(rgb(236, 167, 38), rgb(240, 208, 148))";
            break;
        case 4:
            catPic4.src = "img/kitty.jpg";
            catName4.textContent = "Kitty";
            cardBody4.style.background = "linear-gradient(rgb(182, 178, 181), rgb(230, 223, 228))";
            break;
        case 5:
            catPic4.src = "img/billy.jpg";
            catName4.textContent = "Billy";
            cardBody4.style.background = "linear-gradient(rgb(38, 236, 144), rgb(162, 240, 202))";
            break;
        case 6:
            catPic4.src = "img/birdie.jpg";
            catName4.textContent = "Birdie";
            cardBody4.style.background = "linear-gradient(rgb(124, 221, 250), rgb(208, 241, 252))";
            break;
        default:
            catPic4.src = "img/placeholder.jpg";
            catName4.textContent = "Cat"
            break;
    }

    catRoll4.textContent = number;
    return number;
}

function catCard5() {
    let number = getRandomNumber();
    switch (number) {
        case 1:
            catPic5.src = "img/zuko.jpg";
            catName5.textContent = "Zuko";
            cardBody5.style.background = "linear-gradient(rgb(138, 133, 74), rgb(172, 169, 142))";
            break;
        case 2:
            catPic5.src = "img/jade.jpg";
            catName5.textContent = "Jade";
            cardBody5.style.background = "linear-gradient(rgb(238, 150, 238), rgb(243, 205, 243))";
            break;
        case 3:
            catPic5.src = "img/nut.jpg";
            catName5.textContent = "Nut";
            cardBody5.style.background = "linear-gradient(rgb(236, 167, 38), rgb(240, 208, 148))";
            break;
        case 4:
            catPic5.src = "img/kitty.jpg";
            catName5.textContent = "Kitty";
            cardBody5.style.background = "linear-gradient(rgb(182, 178, 181), rgb(230, 223, 228))";
            break;
        case 5:
            catPic5.src = "img/billy.jpg";
            catName5.textContent = "Billy";
            cardBody5.style.background = "linear-gradient(rgb(38, 236, 144), rgb(162, 240, 202))";
            break;
        case 6:
            catPic5.src = "img/birdie.jpg";
            catName5.textContent = "Birdie";
            cardBody5.style.background = "linear-gradient(rgb(124, 221, 250), rgb(208, 241, 252))";
            break;
        default:
            catPic5.src = "img/placeholder.jpg";
            catName5.textContent = "Cat"
            break;
    }

    catRoll5.textContent = number;
    return number;
}

function handleScore() {
    rollButton.setAttribute("disabled", "true");
    if (currentScore.textContent >= highScore.textContent) {
        highScore.textContent = currentScore.textContent;
    }
}

function handleRoll() {
    if (!hold1) {
        num1 = catCard1();
    }
    if (!hold2) {
        num2 = catCard2();
    }
    if (!hold3) {
        num3 = catCard3();
    }
    if (!hold4) {
        num4 = catCard4();
    }
    if (!hold5) {
        num5 = catCard5();
    }

    count--;
    remainingRollCount.textContent = count;

    score = calculateSum(num1, num2, num3, num4, num5);

    let numbers = [num1, num2, num3, num4, num5];

    let one = 0;
    let two = 0;
    let three = 0;
    let four = 0;
    let five = 0;
    let six = 0;

    for (let i = 0; i <= numbers.length; i++) {
        switch (numbers[i]) {
            case 1:
                one++;
                break;
            case 2:
                two++;
                break;
            case 3:
                three++;
                break;
            case 4:
                four++;
                break;
            case 5:
                five++
                break;
            case 6:
                six++
                break;
            default:
                break;
        }
    }
    
    let counts = [one, two, three, four, five, six]

    for (let i = 0; i < counts.length; i++) {
        if (counts[i] === 3) {
            score += 30;
        }
        if (counts[i] === 4) {
            score += 40;
        }
        if (counts[i] === 5) {
            score += 50;
        }
    }

    currentScore.textContent = String(score);

    if (count === 0) {
        handleScore();
    }
}

function handleHold1() {
    if (hold1) {
        hold1 = false;
        return;
    }
    else if (!hold1) {
        hold1 = true;
        return;
    }
}

function handleHold2() {
    if (hold2) {
        hold2 = false;
        return;
    }
    else if (!hold2) {
        hold2 = true;
        return;
    }
}

function handleHold3() {
    if (hold3) {
        hold3 = false;
        return;
    }
    else if (!hold3) {
        hold3 = true;
        return;
    }
}

function handleHold4() {
    if (hold4) {
        hold4 = false;
        return;
    }
    else if (!hold4) {
        hold4 = true;
        return;
    }
}

function handleHold5() {
    if (hold5) {
        hold5 = false;
        return;
    }
    else if (!hold5) {
        hold5 = true;
        return;
    }
}

function handlePlayAgain() {
    catPic1.src = "img/placeholder.jpg";
    catPic2.src = "img/placeholder.jpg";
    catPic3.src = "img/placeholder.jpg";
    catPic4.src = "img/placeholder.jpg";
    catPic5.src = "img/placeholder.jpg";
    catName1.textContent = "Cat";
    catName2.textContent = "Cat";
    catName3.textContent = "Cat";
    catName4.textContent = "Cat";
    catName5.textContent = "Cat";
    catRoll1.textContent = 0;
    catRoll2.textContent = 0;
    catRoll3.textContent = 0;
    catRoll4.textContent = 0;
    catRoll5.textContent = 0;
    cardBody1.style.background = "white";
    cardBody2.style.background = "white";
    cardBody3.style.background = "white";
    cardBody4.style.background = "white";
    cardBody5.style.background = "white";
    count = 3;
    if (hold1) {
        btnCheck1.click();
    }
    hold1 = false;
    if (hold2) {
        btnCheck2.click();
    }
    hold2 = false;
    if (hold3) {
        btnCheck3.click();
    }
    hold3 = false;
    if (hold4) {
        btnCheck4.click();
    }
    hold4 = false;
    if (hold5) {
        btnCheck5.click();
    }
    hold5 = false;
    remainingRollCount.textContent = count;
    rollButton.removeAttribute("disabled");
    score = 0;
    currentScore.textContent = score;
}

scoreButton.addEventListener("click", handleScore);
rollButton.addEventListener("click", handleRoll);
holdButton1.addEventListener("click", handleHold1);
holdButton2.addEventListener("click", handleHold2);
holdButton3.addEventListener("click", handleHold3);
holdButton4.addEventListener("click", handleHold4);
holdButton5.addEventListener("click", handleHold5);
playAgainButton.addEventListener("click", handlePlayAgain);