import React, {
  useState,
  useEffect,
  type FormEvent,
  type ChangeEvent,
} from "react";
import type { Car, Recommendation, User } from "../../utils/types";

const RecommendTab: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [cars, setCars] = useState<Car[]>([]);
  const [selectedUser, setSelectedUser] = useState<string>("");
  const [selectedCars, setSelectedCars] = useState<number[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchUsers();
    fetchCars();
  }, []);

  async function fetchUsers() {
    try {
      const response = await fetch("https://localhost:7160/api/user/all", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const data: User[] = await response.json();
      setUsers(data);
    } catch (e) {
      console.log(e);
      setError("Oops, Something went wrong.");
    } finally {
      setLoading(false);
    }
  }

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

  async function addRecommendation() {
    if (!selectedCars.length || !selectedUser) {
      return;
    }

    try {
      const recommendation: Recommendation = {
        username: selectedUser,
        recommendedCars: selectedCars,
      };

      const response = await fetch("https://localhost:7160/api/recommend", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(recommendation),
      });

      const data = await response.text();
      setSelectedUser("");
      setSelectedCars([]);
    } catch (e) {
      console.log(e);
      setError("Oops, Something went wrong.");
    }
  }

  function handleSelection(e: ChangeEvent, index: number) {
    if (!(e.target as HTMLInputElement).checked) {
      setSelectedCars(selectedCars.filter((n) => n !== index));
      return;
    }

    setSelectedCars([...selectedCars, index]);
  }

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <>
      <div className="flex items-center justify-between">
        <h1 className="mb-8 px-2 text-3xl font-medium text-indigo-200">
          Select a user and check some cars to make recommendations.
        </h1>

        <select
          value={selectedUser}
          onChange={(e) => setSelectedUser(e.target.value)}
          className="min-w-52 rounded-lg border-4 border-slate-900 bg-slate-900 p-2"
        >
          <option value="" selected>
            Select User
          </option>
          {users.map((user, index) => (
            <option key={index} value={user.username}>
              {user.username}
            </option>
          ))}
        </select>
      </div>

      <section>
        <table className="grid gap-2">
          <thead>
            <tr className="grid grid-cols-[80px_repeat(4,1fr)] rounded-xl bg-slate-800 px-4 py-3">
              <td>Check</td>
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
              <tr
                className="mx-2 grid grid-cols-[80px_repeat(4,1fr)] px-2 py-3"
                key={index}
              >
                <td>
                  <input
                    type="checkbox"
                    onChange={(e) => handleSelection(e, index)}
                  />
                </td>
                <td>{car.name}</td>
                <td>{car.modelNumber}</td>
                <td>{car.color}</td>
                <td>{car.type}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </section>

      <div className="flex items-center justify-end">
        <button
          type="button"
          onClick={addRecommendation}
          className={`block rounded-md px-4 py-2 ${selectedCars.length && selectedUser ? "bg-indigo-500" : "cursor-not-allowed bg-indigo-200 text-indigo-500"}`}
        >
          Add Recommendation
        </button>
      </div>
    </>
  );
};

export default RecommendTab;
