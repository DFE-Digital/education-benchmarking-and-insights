module.exports = {
    transform: {
      '^.+\\.(ts|tsx)$': 'babel-jest',
    },
    testMatch: ['**/?(*.)+(spec|test).(ts|tsx|js)'],
    testEnvironment: 'jsdom',

  };
  