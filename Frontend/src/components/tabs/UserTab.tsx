import React, { useState, useEffect, type FormEvent } from "react";
import type { User } from "../../utils/types";
import ErrorMessage from "../ErrorMessage";

const UserTab: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchUsers();
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

  function updateUsers() {
    fetchUsers();
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
        Here all users of our app.
      </h1>
      <table className="grid gap-2">
        <thead>
          <tr className="grid grid-cols-4 rounded-xl bg-slate-800 px-4 py-3">
            <td>Username</td>
            <td>First Name</td>
            <td>Last Name</td>
            <td className="text-center">Is Activated</td>
          </tr>
        </thead>
        <tbody
          className="grid gap-2 divide-y divide-indigo-300"
          id="recommendation"
        >
          {users.map((user) => (
            <tr className="mx-2 grid grid-cols-4 px-2 py-3" key={user.username}>
              <td>{user.username}</td>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td className="text-center">
                {user.isActive ? (
                  <span className="text-green-400">yes</span>
                ) : (
                  <span className="text-red-600">No</span>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <AddUserForm updateUsers={updateUsers} />
    </>
  );
};

interface AddUserFormProps {
  updateUsers: Function;
}

const AddUserForm: React.FC<AddUserFormProps> = ({ updateUsers }) => {
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const [username, setUsername] = useState<string>("");
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [message, setMessage] = useState<string>("");

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!username || !firstName || !lastName || password.length < 8) {
      return;
    }

    try {
      const response = await fetch("https://localhost:7160/api/user/add", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify({ username, firstName, lastName, password }),
      });

      if (!response.ok) {
        setMessage(await response.text());
      }

      resetForm();
      updateUsers();
    } catch (e) {
      console.error(e);
    }
  }

  function resetForm() {
    setUsername("");
    setFirstName("");
    setLastName("");
    setPassword("");
    setMessage("");
  }

  return (
    <form className="py-4" onSubmit={handleSubmit}>
      {isOpen && (
        <div className="grid grid-cols-4 gap-4 rounded-xl bg-slate-900 px-4 py-3">
          <input
            required
            type="text"
            placeholder="e.g. gold"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            className="rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
          />
          <input
            required
            type="text"
            placeholder="e.g. Gol D."
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            className="rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
          />
          <input
            required
            type="text"
            placeholder="e.g. Roger"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            className="rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
          />
          <input
            required
            type="text"
            placeholder="default: pass1234"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
          />
        </div>
      )}
      <div className="flex items-center gap-4 p-4">
        <div className="mr-auto grid w-96">
          {message && (
            <ErrorMessage message={message} onClear={() => setMessage("")} />
          )}
        </div>
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

export default UserTab;
