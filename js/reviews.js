// get and display reviews
async function loadReviews() {
  // Assuming 'google' and 'Place' are available from the Maps JavaScript API
  if (typeof google === 'undefined' || typeof google.maps.places.Place === 'undefined') {
      console.error("Google Maps Places API not loaded.");
      return;
  }
  const Place = google.maps.places.Place;

  const place = new Place({
    id: "ChIJSwiF3sBPhkgRDnl5tKNudJ0", // your place ID
  });

  await place.fetchFields({
    fields: ["reviews", "displayName"],
  });

  const container = document.getElementById("reviews");

  if (!container) {
    console.log("Can't find reviews container");
    return;
  }

  if (!place.reviews || place.reviews.length === 0) {
    container.innerHTML = "No reviews found.";
    return;
  }

  // 1. Initial HTML: Remove the button from the template
  container.innerHTML = place.reviews
    .map((review, index) => {
      // Add a unique identifier for easier targeting (like data-index)
      return `
        <div class="review flex flex-col overflow-hidden border-4 border-blue-900 max-h-72 snap-start" data-review-index="${index}">
            <div class="flex flex-row items-center gap-2">
              <img src="/images/google-logo.svg">
              <span class="flex flex-row">${'<i class="fa-solid fa-star" style="color: gold;"></i>'.repeat(review.rating)}</span>
            </div>

            <strong>${review.authorAttribution.displayName}</strong> 
            <p class="flex-grow overflow-hidden review-text">${review.text}</p>
        </div>
      `;
    })
    .join("");

  // 2. and 3. Check for overflow and insert button after rendering
  const reviewElements = container.querySelectorAll('.review');

  reviewElements.forEach(reviewEl => {
    const textParagraph = reviewEl.querySelector('.review-text');
    
    // Check if the content height is greater than the visible container height
    // .review-text should not have 'max-height' or 'height' applied, but its parent (.review) should.
    // Ensure the CSS allows this check to be accurate, which is typically the case when parent has max-height and overflow:hidden.
    
    // Use scrollHeight > clientHeight to detect overflow
    if (textParagraph.scrollHeight > textParagraph.clientHeight) {
        
      // 4. Dynamically create and insert the button
      const button = document.createElement('button');
      button.className = "w-fit text-gray-500 resize-button";
      button.textContent = "Read more";
      
      // Set the onclick handler to toggle the classes on the PARENT element
      button.onclick = function() {
        this.textContent = this.textContent.trim() === 'Read more' ? 'Hide' : 'Read more';
        this.parentElement.classList.toggle('max-h-72');
        this.parentElement.classList.toggle('h-fit');
      };

      reviewEl.appendChild(button);
    }
  });
}

document.addEventListener('DOMContentLoaded', function () {
  loadReviews();
})