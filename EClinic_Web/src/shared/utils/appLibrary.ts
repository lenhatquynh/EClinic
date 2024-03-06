class AppLibrary {
  showloading(): void {
    const element = document.querySelector("body")

    // The HTML string to be appended
    const html =
      '<div id="app-loading"><div class="loader">Loading&#8230;</div></div>'

    // append
    element!.insertAdjacentHTML("beforeend", html)
  }

  hideloading(): void {
    const loadingDiv = document.getElementById("app-loading")
    if (loadingDiv) {
      loadingDiv.remove()
    }
  }
}

export const appLibrary = new AppLibrary()
