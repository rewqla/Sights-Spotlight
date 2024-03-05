import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import agent from "../../app/api/agent";
import { Country } from "../../app/models/Country";
import "./Countries.css";
import CountryCard from "./CountryCard";

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
      <h1 className="countries-title">Countries</h1>
      <div className="countries-list">
        {countries.map((country) => (
          <CountryCard key={country.id} country={country} />
        ))}
      </div>
    </div>
  );
};

export default Countries;
