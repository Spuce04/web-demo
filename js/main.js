var navToggled = false;

function ToggleNav() {
  navToggled = !navToggled;

  document.getElementById('nav-list').classList.toggle('hidden');
  document.getElementById('nav-list').classList.toggle('flex');
  document.getElementById('nav-backdrop').classList.toggle('hidden');

  const iconUpper = document.getElementById('icon-upper');
  const iconMiddle = document.getElementById('icon-middle');
  const iconLower = document.getElementById('icon-lower');

  iconUpper.classList.toggle('top-0');
  iconUpper.classList.toggle('rotate-45');
  iconUpper.classList.toggle('top-1/2');
  iconUpper.classList.toggle('-translate-y-1/2');

  iconMiddle.classList.toggle('opacity-0');
  iconMiddle.classList.toggle('opacity-100');

  iconLower.classList.toggle('bottom-0');
  iconLower.classList.toggle('-rotate-45');
  iconLower.classList.toggle('top-1/2');
  iconLower.classList.toggle('-translate-y-1/2');

  document.getElementById('header').style.setProperty('--tw-bg-opacity', (100 * navToggled).toString());
  adjustHeaderOpacity();
}

if ('ontouchstart' in window) {
  document.addEventListener('touchstart', e => {
    const hovered = document.querySelector(':hover');
    // If something is hovered and the touch isn't on it or its children
    if (hovered && !hovered.contains(e.target)) {
      hovered.style.pointerEvents = 'none';
      // Force layout flush so browser drops the hover state
      void hovered.offsetHeight;
      hovered.style.pointerEvents = '';
    }
  }, { passive: true });
}

window.adjustHeaderOpacity = function () {
  const header = document.getElementById('header');
  if (!header) return;

  const maxScroll = 100;
  if (navToggled) { return };

  const opacity = Math.min(window.scrollY / maxScroll, 1);
  console.log(opacity.toString());
  header.style.setProperty('--tw-bg-opacity', opacity.toString());
}

document.addEventListener('DOMContentLoaded', function () {
    // 1. Initial run to set the header's starting opacity based on current scroll position (0)
    adjustHeaderOpacity(); 

    // 2. Set up the scroll listener for future changes
    window.addEventListener('scroll', adjustHeaderOpacity);
});

var TxtType = function (el, toRotate, period) {
  this.toRotate = toRotate;
  this.el = el;
  this.loopNum = 0;
  this.period = parseInt(period, 10) || 2000;
  this.txt = '';
  this.tick();
  this.isDeleting = false;
};

TxtType.prototype.tick = function () {
  var i = this.loopNum % this.toRotate.length;
  var fullTxt = this.toRotate[i];

  if (this.isDeleting) {
    this.txt = fullTxt.substring(0, this.txt.length - 1);
  } else {
    this.txt = fullTxt.substring(0, this.txt.length + 1);
  }

  this.el.innerHTML = '<span class="text-black w-fit text-[clamp(2rem,5vw,6rem)] font-bold block leading-none truncate border-r-2 animate-flashBorder ">' + this.txt + '</span>';

  var that = this;
  var delta = 100 - Math.random() * 50;

  if (this.isDeleting) { delta /= 2; }

  if (!this.isDeleting && this.txt === fullTxt) {
    delta = this.period;
    this.isDeleting = true;
  } else if (this.isDeleting && this.txt === '') {
    this.isDeleting = false;
    this.loopNum++;
    delta = 500;
  }

  setTimeout(function () {
    that.tick();
  }, delta);
};

window.onload = function () {
  var elements = document.getElementsByClassName('typewrite');
  for (var i = 0; i < elements.length; i++) {
    var toRotate = elements[i].getAttribute('data-type');
    var period = elements[i].getAttribute('data-period');
    if (toRotate) {
      new TxtType(elements[i], JSON.parse(toRotate), period);
    }
  }
};

const observer = new IntersectionObserver((entries) => {
  entries.forEach((entry) => {
    if(entry.isIntersecting)
    {
      entry.target.classList.add('appear');
    }
  });
});

const hiddenElements = document.querySelectorAll('.appear-on-observe-left, .appear-on-observe-right');
hiddenElements.forEach((el) => {observer.observe(el)});