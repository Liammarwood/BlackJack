# Black Jack
# Imports the modules
from random import shuffle
total_d_score = 0
total_p_score = 0
ranks = [1,2,3,4,5,6,7,8,9,10,"JACK","QUEEN","KING","ACE"]
suits = ["SPADE","HEART","DIAMOND", "CLUB"]
total_value = 0
score = 0
pscore = 0
def new_deck():
    global deck
    # returns the brand new deck of cards
    deck = []
    for rank in ranks:
        deck.append([rank, suits[0]])
    for rank in ranks:
        deck.append([rank, suits[1]])
    for rank in ranks:
        deck.append([rank, suits[2]])
    for rank in ranks:
        deck.append([rank, suits[3]])
    return deck
    
def new_game():
    global players_hand, dealers_hand, player_in
    # Randomly suffles the deck of cards
    new_deck()
    shuffle(deck)
    # Adds the player in the game
    player_in = True
    # Gives a player and the dealer their first cards
    players_hand = [['ACE', 'DIAMOND'], [7, 'HEART']]#[deck.pop(), deck.pop()]
    dealers_hand = [deck.pop(), deck.pop()]

 #def card_value(card):
 #   rank = card[0]
  #  if rank in ranks[0:-4]:
  #      return int(rank)
   # elif rank is 'ACE':
  #      return 11
   # else:
   #     return 10

def hand_value(hand):
    oglist = [] ###
    for x in range(len(hand)): ## broken
        oglist.append(hand[x][0]) ##
    for x in range(len(oglist)):
                   if oglist[x] == "JACK":
                       for i in range(len(oglist)):
                           if oglist[i][0] == "JACK":
                               oglist[i][0] = 10
                   if oglist[x] == "QUEEN":
                       for i in range(len(oglist)):
                           if oglist[i][0] == "QUEEN":
                               oglist[i][0] = 10
                   if oglist[x] == "KING":
                       for i in range(len(oglist)):
                           if oglist[i] == "KING":
                               oglist[i] = 10
                   if oglist[x] == "ACE":
                       for i in range(len(oglist)):
                           if oglist[i] == "ACE":
                               oglist[i] = 11 
    cardvalue(oglist)
    if total_value < 21:
        return[str(total_value), total_value]
    elif total_value > 21:
        for i in range(len(oglist)):
            if oglist[i] == 11:
                oglist[i]= 1
                cardvalue(oglist)
    if total_value < 21:
        return['BUST!', total_value]
    elif total_value == 21:

        return ['BLACKJACK!',21]
    else:

        return [str(total_value), total_value]
    
def cardvalue(oglist):
    global total_value
    total_value = 0
    for x in range(len(oglist)):
        total_value += oglist[x]
    return total_value

def main_sequence():
    global player_in, players_hand,total_p_score,total_d_score,score, pscore
    game_over = 1
    while player_in:
        player_score = hand_value(players_hand)
        dealer_score = hand_value(dealers_hand)
        print('Currently at',player_score[1],'with the hand',players_hand)
        if hand_value(players_hand)[1] >21:
            break
        if player_in:
            # Getting the players responce to the cards
             response = int(input('Hit or Stick? [Hit = 1, Stick = 0]  '))
             if response == 1:
                 player_in = True
                 new_card = deck.pop()
                 players_hand.append(new_card)
                 print('You draw', new_card)
             else:
                 player_in = False
    if player_score[1] <=21:
        print('Dealer is at',dealer_score[1],'with the hand',dealers_hand)  
    while ((hand_value(dealers_hand)[1]< 18 and hand_value(dealers_hand)[1]< hand_value(players_hand)[1]) or hand_value(dealers_hand)[1]< hand_value(players_hand)[1]) and game_over == 1:
        new_dealer_card = deck.pop()
        dealers_hand.append(new_dealer_card)
        print('Dealer draws', new_dealer_card)
        dealer_score = hand_value(dealers_hand)
        print('Dealer is at',dealer_score[1])
    while game_over == 1:
        if player_score[1] < 22 and dealer_score[1] > 21:
            print('You Win')
            score = 0
            pscore = 2
            game_over = 0
        elif player_score[1] > dealer_score[1]:
            print('You Win')
            score = 0
            pscore = 2
            game_over = 0
        elif player_score[1] == dealer_score[1]:
            print ('You Tied')
            score = 1
            pscore = 1
            game_over = 0
        elif player_score[1] < dealer_score[1]:
            print('You loose')
            score = 2
            pscore = 0
            game_over = 0
    total_d_score += score
    total_p_score += pscore
    playagain = int(input('Would you like to play again? [Yes = 1, No = 0] '))
    if playagain == 1:
        print('Your Total Score Is',total_p_score)
        print('Dealers Total Score Is',total_d_score)
        print('\n\n\n\n\n')
        new_deck()
        new_game()
        main_sequence()
    else:
        print('end')
        
while True:
    new_deck()
    new_game()
    main_sequence()


