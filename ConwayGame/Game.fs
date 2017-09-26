namespace ConwayGame

    [<RequireQualifiedAccess>]
    module ConwayGame = 
        open System;
        open System.Threading.Tasks;
        let width = 40
        let height = 20
        let size = width*height
        let index (i,j) = i+width*j

        let private get (field:bool[]) (i,j) = 
            match (i,j) with
            |(w,h) when w < 0 || w >= width || h < 0 || h >= height -> false
            |_-> field.[index (i,j)]
        
        let private aliveNeighbours field (i,j) = 
            Array.filter (id) [|for m in i-1..i+1 do
                                        for n in j-1..j+1 do
                                            if not (m=i && n=j && get field (m, n)) then yield get field (m,n)|]
            
        let nextGeneration field = 
            [|for j in 0..height-1 do
                for i in 0..width-1 do
                let neighbours = aliveNeighbours field (i,j)
                yield (neighbours.Length = 3 || (neighbours.Length = 2 && get field (i,j)))|]
        
        let printCell cell = 
            match cell with
            | true -> Console.ForegroundColor <- ConsoleColor.Green
                      Console.Write 'X'
                      Console.ForegroundColor <- ConsoleColor.White
                      ()
            | false -> Console.Write ' '    
                       ()
        let print field =
            Console.Clear()
            Console.Write '|'
            for j in 0..height-1 do
                if j > 0 then Console.Write "|\n|"
                for i in 0..width-1 do
                    let cell = get field (i,j)
                    printCell cell
        let gliderField = 
            [|for i in 0..size-1 do yield ( i=index (2,0)
                                        ||i=index (0,1)
                                        ||i=index (2,1)
                                        ||i=index (1,2)
                                        ||i=index (2,2))|]
        
        let launchConway () = 
            let mutable field = gliderField
            for i in 0..60 do
                field <- nextGeneration field
                print field
                System.Threading.Thread.Sleep(500) 
        let launchConwayAsync() = Task.Run launchConway