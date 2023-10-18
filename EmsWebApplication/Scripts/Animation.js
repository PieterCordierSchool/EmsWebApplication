const canvas = document.getElementById('backgroundCanvas');
const ctx = canvas.getContext('2d');
const dots = [];
const maxDotRadius = 7; 
const maxLineRadius = 250; 

canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

const centerDot = {
    x: canvas.width / 2, 
    y: canvas.height / 2, 
    radius: 5, 
    angle: 0, 
    angularSpeed: Math.random() * 0.02, 
    directionChangeTime: Date.now() + Math.random() * 4000, 
};

const secondDot = {
    x: canvas.width / 2 + 740, 
    y: canvas.height / 2, 
    radius: 5, 
    angle: Math.PI / 2, 
    angularSpeed: Math.random() * 0.02, 
    directionChangeTime: Date.now() + Math.random() * 8000, 
};

// Function to move the center dot along a curved path
function moveCenterDot() {
    const currentTime = Date.now();
    if (currentTime >= centerDot.directionChangeTime) {
        centerDot.angularSpeed = (Math.random() - 0.5) * 0.02; 
        centerDot.directionChangeTime = currentTime + Math.random() * 5000; 
    }

    centerDot.angle += centerDot.angularSpeed;
    centerDot.x = canvas.width / 2 + 500 * Math.cos(centerDot.angle);
    centerDot.y = canvas.height / 2 + 450 * Math.sin(centerDot.angle);
}

// Function to move the second dot along a curved path
function moveSecondDot() {
    const currentTime = Date.now();
    if (currentTime >= secondDot.directionChangeTime) {
        secondDot.angularSpeed = (Math.random() - 0.5) * 0.02; 
        secondDot.directionChangeTime = currentTime + Math.random() * 7000; 
    }

    secondDot.angle += secondDot.angularSpeed;
    secondDot.x = canvas.width / 2 + 550 * Math.cos(secondDot.angle);
    secondDot.y = canvas.height / 2 + 450 * Math.sin(secondDot.angle);
}

// Function to draw the center dot
function drawCenterDot() {
    ctx.fillStyle = 'black';
    ctx.beginPath();
    ctx.arc(centerDot.x, centerDot.y, centerDot.radius, 0, Math.PI * 5);
    ctx.fill();
}

// Function to draw the second dot
function drawSecondDot() {
    ctx.fillStyle = 'white';
    ctx.beginPath();
    ctx.arc(secondDot.x, secondDot.y, secondDot.radius, 0, Math.PI * 5);
    ctx.fill();
}

// Function to draw the background and stationary white dots
function drawBackground() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    for (const dot of dots) {
        ctx.beginPath();
        ctx.arc(dot.x, dot.y, dot.radius, 0, Math.PI * 2);

        if (dot.radius > 5) {
            ctx.fillStyle = 'white'; 
        } else {
            ctx.fillStyle = 'lightblue'; 
        }

        ctx.fill();
    }
}


// Function to create lines from the center dot
function createCenterLines() {
    for (const dot of dots) {
        const dx = centerDot.x - dot.x;
        const dy = centerDot.y - dot.y;
        const distance = Math.sqrt(dx * dx + dy * dy);

        if (distance <= maxLineRadius) {
            const controlX1 = dot.x + (Math.random() - 0.5) * 2 * maxLineRadius;
            const controlY1 = dot.y + (Math.random() - 0.5) * 2 * maxLineRadius;
            const controlX2 = centerDot.x + (Math.random() - 0.5) * 2 * maxLineRadius;
            const controlY2 = centerDot.y + (Math.random() - 0.5) * 2 * maxLineRadius;

            ctx.strokeStyle = 'red';
            ctx.beginPath();
            ctx.moveTo(dot.x, dot.y);
            ctx.bezierCurveTo(controlX1, controlY1, controlX2, controlY2, centerDot.x, centerDot.y);
            ctx.stroke();
        }
    }
}

// Function to create blue lines from the second dot
function createBlueLines() {
    for (const dot of dots) {
        const dx = secondDot.x - dot.x;
        const dy = secondDot.y - dot.y;
        const distance = Math.sqrt(dx * dx + dy * dy);

        if (distance <= maxLineRadius) {
            const controlX1 = dot.x + (Math.random() - 0.5) * 2 * maxLineRadius;
            const controlY1 = dot.y + (Math.random() - 0.5) * 2 * maxLineRadius;
            const controlX2 = secondDot.x + (Math.random() - 0.5) * 2 * maxLineRadius;
            const controlY2 = secondDot.y + (Math.random() - 0.5) * 2 * maxLineRadius;

            const gradient = ctx.createLinearGradient(dot.x, dot.y, secondDot.x, secondDot.y);
            gradient.addColorStop(0, 'blue');
            gradient.addColorStop(0.33, 'darkblue');
            gradient.addColorStop(0.66, 'orange');
            gradient.addColorStop(1, 'yellow');


            ctx.strokeStyle = gradient;
            ctx.beginPath();
            ctx.moveTo(dot.x, dot.y);
            ctx.bezierCurveTo(controlX1, controlY1, controlX2, controlY2, secondDot.x, secondDot.y);
            ctx.stroke();
        }
    }
}

// Function to add a new white dot within the radius of the(secondDot)
function addNewDot() {
    if (dots.length < 1000) {
        const newDot = {
            x: Math.random() * canvas.width,
            y: Math.random() * canvas.height,
            radius: Math.random() * maxDotRadius,
        };

        const dx = newDot.x - secondDot.x;
        const dy = newDot.y - secondDot.y;
        const distance = Math.sqrt(dx * dx + dy * dy);

        if (distance <= maxLineRadius) {
            dots.push(newDot);
        }
    }
}

// Function to decrease the size of dots within the radius of the (centerDot)
function decreaseDotsWithinRadius(dotObj, maxRadius) {
    for (let i = dots.length - 1; i >= 0; i--) {
        const dot = dots[i];
        const dx = dotObj.x - dot.x;
        const dy = dotObj.y - dot.y;
        const distance = Math.sqrt(dx * dx + dy * dy);

        if (distance <= maxRadius) {
            if (dot.radius > 1) {
                dot.radius -= 0.1;
            } else {
                dots.splice(i, 1);
            }
        }
    }
}

// Function to increase the size of dots within the radius of the (secondDot)
function increaseDotsWithinRadius(dotObj, maxRadius) {
    for (let i = dots.length - 1; i >= 0; i--) {
        const dot = dots[i];
        const dx = dotObj.x - dot.x;
        const dy = dotObj.y - dot.y;
        const distance = Math.sqrt(dx * dx + dy * dy);

        if (distance <= maxRadius) {
            if (dot.radius < 6) { 
                dot.radius += 0.1;
            }
        }
    }
}

// Update the animate function to use the new functions
function animate() {
    moveCenterDot();
    moveSecondDot();
    decreaseDotsWithinRadius(centerDot, maxLineRadius);
    increaseDotsWithinRadius(secondDot, maxLineRadius);
    drawBackground();
    createCenterLines();
    createBlueLines();
    drawCenterDot();
    drawSecondDot();
    addNewDot();
    requestAnimationFrame(animate);
}

animate(); 