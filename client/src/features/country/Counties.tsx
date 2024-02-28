import { useState, useEffect } from "react";
import agent from "../../app/api/agent";
import { Country } from "../../app/models/Country";
import "./Countries.css";

const Countries = () => {
  const [countries, setCountries] = useState<Country[]>([]);

  useEffect(() => {
    const fetchCountries = async () => {
      try {
        const response = await agent.Country.countries();

        setCountries(response);
      } catch (error) {
        console.error(error);
      }
    };
    fetchCountries();
  }, []);

  return (
    <div className="countries-container">
      <h1 className="countries-title">Countries</h1>{" "}
      <ul className="countries-list">
        {countries.map((country) => (
          <li key={country.id} className="country-item">
            {country.name}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Countries;
