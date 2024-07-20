import { useStore } from "@nanostores/react";
import { type WritableAtom } from "nanostores";

type Updater<T> = T | ((prevState: T) => T);

const useNanoStor = <T>(store: WritableAtom<T>): [T, (updater: Updater<T>) => void] => {
  const state = useStore(store);

  const setState = (updater: Updater<T>) => {
    if (typeof updater === 'function') {
      store.set((updater as (prevState: T) => T)(state));
    } else {
      store.set(updater);
    }
  };

  return [state, setState];
};

export default useNanoStor;
