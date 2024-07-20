export type Fuel = "Gas" | "Electrical";

export interface Car {
  name: string;
  modelNumber: number;
  color: string;
  type: Fuel;
}
export interface User {
  username: string;
  firstName: string;
  lastName: string;
}

export interface Recommendation {
  username: string;
  recommendedCars: number[];
}
