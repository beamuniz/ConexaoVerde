module.exports = {
  content: [
    "./Views/**/*.cshtml",    // Incluir todos os arquivos CSHTML dentro da pasta Views
    "./wwwroot/js/**/*.js",     // Incluir arquivos JavaScript se você estiver usando Tailwind com JS
    "./wwwroot/css/**/*.css",   // Incluir arquivos CSS
  ],
  theme: {
    extend: {
      colors: {
        'background': '#b8b99a',
        'header': '#3b4a2b'
      },
    },
  },
  plugins: [],
}
