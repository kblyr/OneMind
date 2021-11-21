const colors = require('tailwindcss/colors')

module.exports = {
    purge: {
        enabled: false,
        content: [
            './**/*.html',
            './**/*.razor'
        ],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        colors: {
            theme: colors.amber,
            white: colors.white,
            black: colors.black,
            gray: colors.blueGray
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
}
