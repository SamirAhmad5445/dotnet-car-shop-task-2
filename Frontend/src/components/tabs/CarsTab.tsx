import React, { useState, useEffect, type FormEvent } from "react";
import type { Car, Fuel } from "../../utils/types";

const CarsTab: React.FC = () => {
  const [cars, setCars] = useState<Car[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchCars();
  }, []);

  async function fetchCars() {
    try {
      const response = await fetch("https://localhost:7160/api/car/all", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const data: Car[] = await response.json();
      setCars(data);
    } catch (e) {
      console.log(e);
      setError("Oops, Something went wrong.");
    } finally {
      setLoading(false);
    }
  }

  function updateCars() {
    fetchCars();
  }

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <>
      <h1 className="mb-8 px-2 text-3xl font-medium text-indigo-200">
        Here all cars in out shop.
      </h1>
      <table className="grid gap-2">
        <thead>
          <tr className="grid grid-cols-4 rounded-xl bg-slate-800 px-4 py-3">
            <td>Car Name</td>
            <td>Model Number</td>
            <td>Color</td>
            <td>Type</td>
          </tr>
        </thead>
        <tbody
          className="grid gap-2 divide-y divide-indigo-300"
          id="recommendation"
        >
          {cars.map((car, index) => (
            <tr className="mx-2 grid grid-cols-4 px-2 py-3" key={index}>
              <td>{car.name}</td>
              <td>{car.modelNumber}</td>
              <td>{car.color}</td>
              <td>{car.type}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <AddCarForm updateCars={updateCars} />
    </>
  );
};

interface AddCarFormProps {
  updateCars: Function;
}

const AddCarForm: React.FC<AddCarFormProps> = ({ updateCars }) => {
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const [name, setName] = useState<string>("");
  const [modelNumber, setModelNumber] = useState<number>(0);
  const [color, setColor] = useState<string>("");
  const [type, setType] = useState<Fuel>("Gas");

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!name || !modelNumber || !color || !type) {
      return;
    }

    try {
      const response = await fetch("https://localhost:7160/api/car/add", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify({ name, modelNumber, color, type }),
      });

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const data: string = await response.text();
      updateCars();
    } catch (e) {
      console.error(e);
    }
  }

  return (
    <form className="py-4" onSubmit={handleSubmit}>
      {isOpen && (
        <div className="grid grid-cols-4 gap-4 rounded-xl bg-slate-900 px-4 py-3">
          <input
            required
            type="text"
            placeholder="e.g. Sunny"
            value={name}
            onChange={(e) => setName(e.target.value)}
            className="rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
          />
          <input
            required
            type="number"
            placeholder="e.g. 2014"
            value={modelNumber}
            onChange={(e) => setModelNumber(Number(e.target.value))}
            className="rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
          />
          <input
            required
            type="text"
            placeholder="e.g. Red"
            value={color}
            onChange={(e) => setColor(e.target.value)}
            className="rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
          />
          <select
            value={type as string}
            onChange={(e) => setType(e.target.value as Fuel)}
            className="bg-transparent"
          >
            <option value="Gas" selected className="text-slate-900">
              Gas
            </option>
            <option value="Electrical" className="text-slate-900">
              Electrical
            </option>
          </select>
        </div>
      )}
      <div className="flex justify-end gap-4 p-4">
        <button
          type="button"
          onClick={() => setIsOpen((is) => !is)}
          className={`block rounded-md px-4 py-2 ${isOpen ? "bg-indigo-200 text-indigo-500" : "bg-indigo-500"}`}
        >
          {isOpen ? "Cancel" : "Add User"}
        </button>

        {isOpen && (
          <button className="block rounded-md bg-indigo-500 px-4 py-2">
            Create
          </button>
        )}
      </div>
    </form>
  );
};

export default CarsTab;
