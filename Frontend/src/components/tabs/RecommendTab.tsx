import React, { useState, useEffect, type FormEvent } from "react";
import type { Car, Recommendation, User } from "../../utils/types";

const RecommendTab: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [cars, setCars] = useState<Car[]>([]);
  const [selectedUser, setSelectedUser] = useState<string>("");
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
    </>
  );
};

export default RecommendTab;
