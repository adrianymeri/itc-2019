namespace Timetabling.Common

type IRandom =
    abstract Next : int -> int
    abstract NextDouble : unit -> float

module Random =
    let private initial = System.Random()

    let ofObjectSafe (random: System.Random) =
        { new IRandom with
            member __.Next(max: int) : int = lock random (fun () -> random.Next max)

            member __.NextDouble() =
                lock random (fun () -> random.NextDouble()) }

    let ofSeedSafe (seed: int) = System.Random seed |> ofObjectSafe

    let ofObject (random: System.Random) =
        { new IRandom with
            member __.Next(max: int) : int = random.Next max
            member __.NextDouble() = random.NextDouble() }

    let ofSeed (seed: int) = System.Random seed |> ofObject

    let toInt (n: int) (rand: float) = (float n) * rand |> int

    let next (random: IRandom) = random.NextDouble()

    let nextN n (random: IRandom) = random.Next n

    let nextBool (random: IRandom) = random.Next 2 = 0

    let nextIndex (random: IRandom) (array: 'a []) = array.[random |> nextN array.Length]

    let nextInt () = lock initial (fun () -> initial.Next())

    let nextIntN max =
        lock initial (fun () -> initial.Next max)
