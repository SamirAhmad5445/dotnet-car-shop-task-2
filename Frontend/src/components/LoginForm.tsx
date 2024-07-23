import type React from "react";
import {
  useState,
  type ErrorInfo,
  type FormEvent,
  type MouseEventHandler,
} from "react";
import { getRoute } from "../utils";
import ErrorMessage from "./ErrorMessage";

const LoginForm: React.FC = () => {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string>("");

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!username) {
      setError("Please enter you username.");
      return;
    }

    if (!password || password.length < 8) {
      setError("Please enter you password (at least 8 characters).");
      return;
    }

    try {
      const response = await fetch(`https://localhost:7160/api/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        const message = await response.text();
        setError(message);
      }

      const { role, isActive } = await response.json();
      console.log();
      location.href = getRoute(role, isActive);
    } catch (e) {
      console.error(e);
    }
  }

  return (
    <form onSubmit={handleSubmit} className="grid gap-4">
      <input
        className="flex-1 rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
        type="text"
        placeholder="Username"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <input
        className="flex-1 rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
        type="password"
        placeholder="Password"
        min={8}
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      {error && <ErrorMessage message={error} onClear={(e) => setError("")} />}
      <button className="rounded-md bg-indigo-500 px-4 py-2">Login</button>
    </form>
  );
};

export default LoginForm;
