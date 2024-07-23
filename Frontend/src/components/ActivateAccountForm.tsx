import { useState, type FormEvent } from "react";
import ErrorMessage from "./ErrorMessage";
import { getRoute } from "../utils";

const ActivateAccountForm = () => {
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [error, setError] = useState<string>("");

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!password || password.length < 8) {
      setError("Please enter you password (at least 8 characters).");
      return;
    }

    if (confirmPassword !== password) {
      setError("Password mismatch, please confirm your password.");
      return;
    }

    try {
      const response = await fetch(`https://localhost:7160/api/user/activate`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(password),
      });

      if (!response.ok) {
        const message = await response.text();
        setError(message);
      }

      location.replace(getRoute("user", true));
    } catch (e) {
      console.error(e);
    }
  }

  return (
    <form onSubmit={handleSubmit} className="grid gap-4">
      <input
        className="flex-1 rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
        type="password"
        placeholder="New Password"
        min={8}
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <input
        className="flex-1 rounded-lg border border-indigo-200 bg-transparent px-4 py-2 caret-indigo-400 outline-0 focus:border-indigo-400"
        type="password"
        placeholder="Confirm password"
        value={confirmPassword}
        onChange={(e) => setConfirmPassword(e.target.value)}
      />
      {error && <ErrorMessage message={error} onClear={(e) => setError("")} />}
      <button className="rounded-md bg-indigo-500 px-4 py-2">Login</button>
    </form>
  );
};

export default ActivateAccountForm;
