export const generateRandomCountryName = () => {
  return `Country-${Math.random().toString(36).substring(7)}`;
};
