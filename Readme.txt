Scorciatoie per cambiare salvataggi del gioco attraverso file:
	-livello -> indicare il nome del livello desiderato (Tutorial,FirstChapter,SecondChapter,BonusChapter,FinalChapter,ExtendedChapter)
	-aspetto personaggio -> selezionare 0 o 1
	-difficoltà -> indicare la difficoltà desiderata (easy,medium,difficult,extreme)

Scorciatoie per cambiare configurazione del gioco attraverso file:
	-aspetto personaggio (0 o 1)
	-sensibilità mouse (intero)
	-inversione asse x (booleano:false(disattivo),true(attivo))
	-inversione asse y (booleano:false(disattivo),true(attivo))
	-risoluzione orizzontale (intero)
	-risoluzione verticale (intero)
	-antialiasing (intero: 0,2,4,8)
	-dimensione schermo (booleano:false(full),true(finestra))
	-risoluzione ombre (intero:256,512,1024,2048,4096)
	-distanza ombre (float:10,30,50,80,100)
	-ombre (booleano:false(disattivo),true(attivo))
	-qualità di rendering (float:da 0 a 200)
	-v-sync (booleano:false(disattivo),true(attivo))
	-fps (booleano:false(disattivo),true(attivo))
	-volume musica (float:da 0 a 100)
	-volume nemici (float:da 0 a 100)
	-volume personaggio (float:da 0 a 100)
	-rendering pipeline (intero: da 0 a 5)

Per ottenere i seguenti file svolgere le seguenti azioni:
	-file del salvataggio: avviare una partita e dal menu premere su esci
	-file di configurazione: entrare ed uscire dalle impostazioni o premere il pulsante torna al desktop

Nel progetto condiviso non è presente il supporto al formato 21:9 che è stato aggiunto in seguito modificando semplicemente gli elementi dell'UI.
Ma il supporto è presente nella build.

Se nell'editor si prova a eseguire il gioco in scene diverse da quella princiaple il gioco funzionerà ma non in modo ottimale per mancanza di inizializzazione di parametri fondamentali.
(nel progetto nel tutorial (in editor) si partirà con l'ottava fase, per correggere l'esecuzione cambiare il valore del parametro in riga 34 dello script TutorialManager.cs da 8 a 0)