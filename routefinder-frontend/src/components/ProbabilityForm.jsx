import React, { useState } from "react";
import { Grid } from "@mui/material";

const ProbabilityForm = (props) => {
  const [inputA, setInputA] = useState("");
  const [inputB, setInputB] = useState("");
  const [calculationValue, setCalculationValue] = useState("");

  const handleInputAChange = (event) => {
    setInputA(event.target.value);
  };

  const handleInputBChange = (event) => {
    setInputB(event.target.value);
  };

  const handleCalculationChange = (event) => {
    setCalculationValue(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log(
      `inputA: ${inputA}, inputB: ${inputB}, chosen calculation: ${calculationValue} `
    );
  };

  return (
    <>
      <h1>Probability Calculator</h1>
      <p>Please enter your figures and choose your calculation</p>
      {/* I need two inputs for the figures and a Select for the type of calculation */}
      <Grid container justifyContent="center" alignItems="center">
        <form onSubmit={handleSubmit}>
          <Grid item>
            <label>
              Probability of A: P(A)
              <input type="text" value={inputA} onChange={handleInputAChange} />
            </label>
          </Grid>
          <Grid item>
            <label>
              Probability of B: P(B)
              <input type="text" value={inputB} onChange={handleInputBChange} />
            </label>
          </Grid>
          <Grid item>
            <label>
              Pick your calculation type:
              <select
                value={calculationValue}
                onChange={handleCalculationChange}
              >
                <option value="" hidden>
                  Please select
                </option>
                <option value="CombinedWith">Combined With: P(A)P(B)</option>
                <option value="Either">Either: P(A)+P(B) - P(A)P(B)</option>
              </select>
            </label>
          </Grid>
          <Grid item>
            <input type="submit" value="Submit" />
          </Grid>
        </form>
      </Grid>
    </>
  );
};

export default ProbabilityForm;
