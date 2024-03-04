import { Link } from "react-router-dom";
import { Country } from "../../app/models/Country";
import "./CountryCard.css";

interface Props {
  country: Country;
}

const CountryCard = ({ country }: Props) => {
  const imageUrl = `https://localhost:7188/${country.imageURL}`;

  return (
    <div className="country-card">
      <img src={imageUrl} alt={country.name} className="country-image" />
      <div className="country-details">
        <h3>{country.name}</h3>
        <button className="explore-button">Explore country</button>
      </div>
    </div>
  );
};

export default CountryCard;
