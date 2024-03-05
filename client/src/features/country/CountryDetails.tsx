import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import agent from "../../app/api/agent";

const CountryDetails = () => {
  const { id } = useParams<{ id: string }>();
  const [countryDetails, setCountryDetails] = useState<any>(null);

  useEffect(() => {
    const fetchCountryDetails = async () => {
      try {
        const response = await agent.Country.countryDetails(parseInt(id!));
        setCountryDetails(response);
      } catch (error) {
        console.error("Error fetching country details:", error);
      }
    };
    console.log(1);

    fetchCountryDetails();
  }, [id]);

  if (!countryDetails) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h1>{countryDetails.name}</h1>
      <p>Description: {countryDetails.description}</p>
    </div>
  );
};

export default CountryDetails;
